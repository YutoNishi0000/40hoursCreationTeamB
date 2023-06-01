using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(TimerUI))]
public class ScreenShot : MonoBehaviour
{
    //入力が必要なもの
    [SerializeField] private RawImage targetImage;     //テクスチャを表示するためのRawImage
    [SerializeField] private Image point1;             //スクショした画像の１番目の移動先
    [SerializeField] private Image point2;             //スクショした画像の２番目の移動先
    [SerializeField] private Image plusCount;
    [SerializeField] private Image minusCount1;
    [SerializeField] private Image minusCount2;
    [SerializeField] private float duration;           //カメラシェイクの継続時間
    [SerializeField] private float magnitude;          //カメラシェイクの揺れの強さ
    [SerializeField]private GameObject mimic = null;    //対象のモデル
    [SerializeField] private GameObject player;
    [SerializeField] private Image lostTimeImg;
    [SerializeField] private GameObject[] gameUI;     //写真を撮るときに消したいUI

    //内部処理で使うもの
    private Camera cam;                                //プレイヤーのカメラ
    private string screenShotPath;                     //スクリーンショットして生成されたテクスチャのファイルパス
    private string timeStamp;                          //現在時刻を表すためのもの
    private const float firstScale = 0.8f;             //一回目移動するときにどれだけRawImnageが縮小されるか（何倍の大きさになるか）
    private const float secondScale = 0.2f;            //二回目縮小するときにどれだけRawImageが縮小されるか（何倍の大きさになるか）
    private Vector3 InitialPrevPos;                    //RawImageの初期位置
    private Vector3 InitialPrevscale;                  //RawImageの初期スケール
    private List<GameObject> setterObj;                //毎フレーム送られてくる異質なものの情報を取得するためのもの
    private bool noneStrangeFlag;                      //異質なものが撮影されていたらfalse 撮影されていなかったらtrue
    public static bool noneTargetFlag;                 //ターゲットが撮影されていたらfalse 撮影されていなかったらtrue
    private readonly float fadeInSpeed = 0.2f;         //フェードスピード、撮影判定呼び出し時間
    private Vector3 center = Vector3.zero;             //画面の中心
    private const float raiseScore = 1.5f;             //スコア上昇倍率
    private SkillManager skillManager;
    private TimerUI fadeManager;
    private readonly float prevSlide = 1.0f;
    private readonly float prevScale = 0.5f;

    //シャッターアニメーションの種類
    private enum ShutterAnimationType
    {
        None,
        Target,
        Other
    }

    //それぞれのスコアの値
    public enum ScoreType
    {
        high = 50,
        midle = 30,
        low = 10,
        outOfScreen = 0,
    };

    //それぞれの判定の幅
    private const float areaWidth = 192;      //(960 / 5) 画面5等分
    private const float areaHeight = 216;     //(1080 / 5) 画面5等分

    private void Start()
    {
        setterObj = new List<GameObject>();
        InitialPrevPos = targetImage.rectTransform.position;
        InitialPrevscale = targetImage.rectTransform.localScale;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        targetImage.enabled = false;
        noneStrangeFlag = true;
        noneTargetFlag = true;
        skillManager = GetComponent<SkillManager>();
        fadeManager = GetComponent<TimerUI>();
        //画面の中心を求める
        center = new Vector3(areaWidth * 2 + areaWidth * 0.5f, areaHeight * 2 + areaHeight * 0.5f, 0.0f);
    }

    private void Update()
    {
        if (Shutter.isFilming)
        {
            OffUIShutter();
            InitializeRawImage();
            ClickShootButton();
            Invoke(nameof(FirstMovePreview), prevSlide);
            ShutterManager();
            ShutterOther();

            //空撮り（異質なもの、ターゲットが撮影されていない）していたら
            if (noneTargetFlag && noneStrangeFlag)
            {
                ShutterNone();
            }

            ShutterAnimationController(fadeInSpeed);

            //フラグを初期化
            noneTargetFlag = true;
            noneStrangeFlag = true;

            //シャッターアニメーションを遅れて表示させる
            //消していたUIをオンに
            Invoke(nameof(OnUIShutter), fadeInSpeed);
            Shutter.isFilming = false;
        }
    }

    #region 撮影系

    //撮影判定関数
    private void ShutterManager()
    {
        //障害物がない時
        if (!createRay())
        { 
            switch (checkScore(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position)))
            {
                case ScoreType.low:
                    ShutterTarget();
                    GameManager.Instance.numLowScore++;
                    break;
                case ScoreType.midle:
                    ShutterTarget();
                    GameManager.Instance.numMiddleScore++;
                    break;
                case ScoreType.high:
                    ShutterTarget();
                    GameManager.Instance.numHighScore++;
                    break;
                case ScoreType.outOfScreen:
                    SEManager.Instance.PlayShot();
                    noneTargetFlag = true;
                    Debug.Log("画面外");
                    break;
            }
        }
        else
        {
            SEManager.Instance.PlayShot();
        }
    }

    //撮影時の処理
    public void ShutterTarget()
    {
        GameObject targetObj = RespawTarget.GetCurrentTargetObj();
        //手動でターゲットが表示される位置を調整
        Vector3 targetPos = new Vector3(targetObj.transform.position.x, targetObj.transform.position.y - 1, targetObj.transform.position.z);
        Instantiate(mimic, targetPos, targetObj.transform.rotation);
        noneTargetFlag = false;
        //時間を獲得
        CountDownTimer.IncreaceTime();
        //ターゲットが撮影された
        SetPhotographTargetFlag(false);
        //スコア加算
        ScoreManger.Score += FinalScorePoint(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position));
        //対象を撮影した回数をインクリメント
        GameManager.Instance.numTargetShutter++;
        //SE
        SEManager.Instance.PlayTargetShot();
        //ターゲットリスポーン
        RespawTarget.RespawnTarget();
        //UIをフェードアウト
        fadeManager.FadeOut(fadeInSpeed, plusCount);
        SEManager.Instance.PlayPlusTimeCountSE();
    }

    private void ShutterOther()
    {
        //サブカメラ撮影判定がオンだったときの判定
        for (int i = 0; i < setterObj.Count; i++)
        {
            if (setterObj[i] != null && JudgeSubTarget(cam, setterObj[i], player))
            {
                noneStrangeFlag = false;
                //サブカメラカウントをインクリメント
                GameManager.Instance.numSubShutter++;
                //スコアを加算
                ScoreManger.Score += 10;
                //tempList[i]のオブジェクトの消滅フラグをオンにする
                setterObj[i].GetComponent<HeterogeneousController>().SetTakenPicFlag(true);
            }
        }
    }

    private void ShutterNone()
    {
        //Debug.Log("時間を失いました");
        Debug.Log("何も撮影できてない");
        CountDownTimer.DecreaceTime();
        player.GetComponent<Player>().Shake(duration, magnitude);
        //ShutterAnimation.NoneAnimationStart();
        fadeManager.FadeOut(fadeInSpeed, minusCount1);
        SEManager.Instance.PlayMinusTimeCountSE();

        //もし難易度がハードだったら
        if (GameManager.Instance.GetGameMode() == GameManager.GameMode.Hard)
        {
            //もう５秒制限時間を減らす
            CountDownTimer.DecreaceTime();
            fadeManager.FadeOut(fadeInSpeed, minusCount2);
        }
    }

    //シャッターアニメーションを管理する関数
    private void ShutterAnimationController(float invokeTime)
    {
        //異質な物だけ撮影した場合
        if (!noneStrangeFlag && noneTargetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Other, invokeTime).Forget();
        }
        //ターゲットだけ撮影した場合
        else if (noneStrangeFlag && !noneTargetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Target, invokeTime).Forget();
        }
        //どちらも撮影した場合
        else if (!noneStrangeFlag && !noneTargetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Target, invokeTime).Forget();
        }
        //空撮りだった場合
        else
        {
            ShutterAnimationmanager(ShutterAnimationType.None, invokeTime).Forget();
        }
    }

    //撮った写真のファイルパスを取得
    private string GetScreenShotPath()
    {
        string directoryPath = GameManager.Instance.GetDirectryPath();

        string path = GameManager.Instance.GetPicturesFilePath(directoryPath) + timeStamp + ".png";

        return path;
    }

    private IEnumerator CreateScreenShot()
    {
        DateTime date = DateTime.Now;
        timeStamp = date.ToString("yyyy-MM-dd-HH-mm-ss-fff");
        // レンダリング完了まで待機
        yield return new WaitForEndOfFrame();

        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cam.targetTexture = renderTexture;

        Texture2D texture = new Texture2D(cam.targetTexture.width / 2, cam.targetTexture.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0, 0, cam.targetTexture.width / 2, cam.targetTexture.height), 0, 0);
        texture.Apply();

        // 保存する画像のサイズを変えるならResizeTexture()を実行
        //		texture = ResizeTexture(texture,320,240);

        byte[] pngData = texture.EncodeToPNG();
        screenShotPath = GetScreenShotPath();

        // ファイルとして保存するならFile.WriteAllBytes()を実行
        File.WriteAllBytes(screenShotPath, pngData);

        cam.targetTexture = null;

        //生成したテクスチャファイルから情報を読み込んでRawImageに出力
        ShowSSImage();
    }

    //撮影関数
    public void ClickShootButton()
    {
        StartCoroutine(CreateScreenShot());
    }

    //撮影した写真をRawImageに表示
    public void ShowSSImage()
    {
        if (!String.IsNullOrEmpty(screenShotPath))
        {
            byte[] image = File.ReadAllBytes(screenShotPath);

            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(image);

            // NGUI の UITexture に表示
            RawImage target = targetImage.GetComponent<RawImage>();
            target.texture = tex;

            //テクスチャ情報を読み込んだ後でRawImageを表示する
            targetImage.enabled = true;
        }
    }

    #endregion

    #region UI系

    //撮影する瞬間非表示にされたUIを表示する関数（Invokeで呼ぶ）
    public void OnUIShutter()
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(true);
        }
    }
    
    //撮影する瞬間非表示にされたUIを表示する関数（Invokeで呼ぶ）
    public void OffUIShutter()
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(false);
        }
    }

    private void InitializeRawImage()
    {
        targetImage.texture = null;
        targetImage.enabled = false;
        targetImage.rectTransform.position = InitialPrevPos;
        targetImage.rectTransform.localScale = InitialPrevscale;
    }

    #endregion

    #region プレビューの動き
    private void FirstMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * firstScale, prevScale);
        Invoke(nameof(SecondMovePreview), 0.5f);
    }

    private void SecondMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * secondScale, prevScale);
        targetImage.rectTransform.DOMove(point1.rectTransform.position, prevScale);
        Invoke(nameof(SlideMovePreview), prevSlide);
    }

    private void SlideMovePreview()
    {
        targetImage.transform.DOMoveX(point2.rectTransform.position.x, prevScale);
    }

    #endregion

    #region シャッターアニメーション

    private async UniTask ShutterAnimationmanager(ShutterAnimationType type, float delayTime)
    {
        int time = (int)(delayTime * 1000);
        await UniTask.Delay(time);

        switch(type)
        {
            case ShutterAnimationType.None:
                ShutterAnimation.NoneAnimationStart();
                break;
            case ShutterAnimationType.Target:
                ShutterAnimation.TargetAnimationStart();
                break;
            case ShutterAnimationType.Other:
                ShutterAnimation.OtherAnimationStart();
                break;
        }
    }

    #endregion

    #region ターゲット撮影判定

    /// <summary>
    /// ワールド座標をスクリーン座標に
    /// </summary>
    /// <param name="cam">カメラオブジェクト</param>
    /// <param name="worldPosition">ワールド座標</param>
    /// <returns>スクリーン座標</returns>
    private Vector3 WorldToScreenPoint(Camera cam, Vector3 worldPosition)
    {
        // カメラ空間に変換(カメラから見た座標に変換)
        Vector3 cameraSpace = cam.worldToCameraMatrix.MultiplyPoint(worldPosition);

        // クリッピング空間に変換(cameraSpaceを一定の範囲に絞ってる)
        Vector4 clipSpace = cam.projectionMatrix * new Vector4(cameraSpace.x, cameraSpace.y, cameraSpace.z, 1f);

        //デバイス座標：左下ー1　右上＋1
        //割ってるのは正規化
        // デバイス座標系に変換
        Vector3 deviceSpace = new Vector3(clipSpace.x / clipSpace.w, clipSpace.y / clipSpace.w, clipSpace.z / clipSpace.w);

        // スクリーン座標系に変換
        Vector3 screenSpace = new Vector3((deviceSpace.x + 1f) * 0.25f * Screen.width, (deviceSpace.y + 1f) * 0.5f * Screen.height, deviceSpace.z);

        return screenSpace;
    }

    /// <summary>
    /// 最終的なスコアポイント
    /// </summary>
    public float FinalScorePoint(Vector3 scrPoint)
    {
        //return lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));
        float finalScore = (float)checkScore(scrPoint);

        //スキルによるスコア加算フラグがオンだったらif内の処理を実行
        if (skillManager.GetAddScoreFlag())
        {
            //フラグがオンだったら1.5倍のスコアを返す
            return finalScore * raiseScore;
        }
        else
        {
            return finalScore;
        }
    }

    /// <summary>
    /// 中央にどれだけ近いか判定
    /// </summary>
    /// <param name="scrPoint">スクリーン座標</param>
    /// <returns>スコアの値</returns>
    private ScoreType checkScore(Vector3 scrPoint)
    {
        //return lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));
        ScoreType defaultScore = lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));

        return defaultScore;
    }
    /// <summary>
    /// スコアの判定(横)
    /// </summary>
    /// <param name="scrPoint">スクリーン座標</param>
    /// <param name="score">タテだけで見たときのスコア</param>
    /// <returns>スコア</returns>
    private ScoreType checkScoreHori(Vector3 scrPoint)
    {
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2)
        {
            return ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth)
        {
            return ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth * 2)
        {
            return ScoreType.low;
        }
        return ScoreType.outOfScreen;
    }
    /// <summary>
    /// スコアの判定(縦)
    /// </summary>
    /// <param name="scrPoint">スクリーン座標</param>
    /// <param name="score">タテだけで見たときのスコア</param>
    /// <returns>スコア</returns>
    private ScoreType checkScoreVart(Vector3 scrPoint)
    {
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2)
        {
            return ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2 + areaHeight)
        {
            return ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2 + areaHeight * 2)
        {
            return ScoreType.low;
        }
        return ScoreType.outOfScreen;
    }
    /// <summary>
    /// 低いほうの値をを求める
    /// </summary>
    /// <param name="v">片方のスコア</param>
    /// <returns>スコア</returns>
    private ScoreType lower(ScoreType v1, ScoreType v2)
    {
        if (v1 < v2)
        {
            return v1;
        }
        else
        {
            return v2;
        }
    }
    RaycastHit hit;
    private bool createRay()
    {
        Vector3 diff = RespawTarget.GetCurrentTargetObj().transform.position - player.gameObject.transform.position;

        Vector3 direction = diff.normalized;
        //Debug.Log(direction);
        float distance = Vector3.Distance(RespawTarget.GetCurrentTargetObj().transform.position, player.transform.position);
        Debug.DrawRay(player.transform.position, distance * direction, Color.green, 1f);
        if (Physics.Raycast(player.transform.position, direction, out hit, distance))
        {
            //Physics.Raycast(player.transform.position, direction, out hit, distance);
            // spere.transform.position = hit.point;
            Debug.Log("ステージに当たってる");
            return true;
        }
        Debug.Log("ステージに当たってない");
        return false;

    }

    #endregion

    #region 異質なもの撮影判定

    public bool JudgeSubTarget(Camera camera, GameObject subTargetPos, GameObject playerPos)
    {
        float fov = camera.fieldOfView;
        float judgeRange = Mathf.Cos(Mathf.PI - (((2 * Mathf.PI) - ((fov / 360) * Mathf.PI * 2)) / 2));
        Vector3 playerToSubVec = subTargetPos.transform.position - playerPos.transform.position;
        Vector3 playerForwardVec = camera.transform.forward;   //念のためカメラが向いている方向のベクトルを取得
        float dot = Vector3.Dot(playerToSubVec.normalized, playerForwardVec.normalized);

        if (playerToSubVec.magnitude < 7.0f && dot >= judgeRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region ゲッター、セッター

    public void SetList(List<GameObject> list) { setterObj = list; }

    public void SetPhotographTargetFlag(bool flag) { noneTargetFlag = flag; }

    #endregion
}
