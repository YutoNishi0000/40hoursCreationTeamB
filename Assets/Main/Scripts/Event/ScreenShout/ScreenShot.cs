using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

[RequireComponent(typeof(TimerUI))]
public class ScreenShot : MonoBehaviour
{
    //画面の中心
    static Vector3 center = Vector3.zero;
    private const float raiseScore = 1.5f;     //スコア上昇倍率

    //入力が必要なもの
    [SerializeField] private RawImage targetImage;     //テクスチャを表示するためのRawImage
    [SerializeField] private Image point1;             //スクショした画像の１番目の移動先
    [SerializeField] private Image point2;             //スクショした画像の２番目の移動先
    [SerializeField] private float duration;           //カメラシェイクの継続時間
    [SerializeField] private float magnitude;          //カメラシェイクの揺れの強さ

    //内部処理で使うもの
    private Camera cam;                                //プレイヤーのカメラ
    private string screenShotPath;                     //スクリーンショットして生成されたテクスチャのファイルパス
    private string timeStamp;                          //現在時刻を表すためのもの
    private const float firstScale = 0.8f;             //一回目移動するときにどれだけRawImnageが縮小されるか（何倍の大きさになるか）
    private const float secondScale = 0.2f;            //二回目縮小するときにどれだけRawImageが縮小されるか（何倍の大きさになるか）
    private Vector3 InitialPrevPos;                    //RawImageの初期位置
    private Vector3 InitialPrevscale;                  //RawImageの初期スケール
    private List<GameObject> setterObj;                //毎フレーム送られてくる異質なものの情報を取得するためのもの
    private List<int> destroyStrangeList;
    [SerializeField]private GameObject mimic = null;    //対象のモデル
    private bool noneStrangeFlag;
    public static bool noneTargetFlag;
    private Player player;
    [SerializeField] private Image lostTimeImg;
    float imgTime;
    float a_img;
    private readonly float fadeInSpeed = 10.0f;
    private SkillManager skillManager;

    [SerializeField] private GameObject[] gameUI;     //写真を撮るときに消したいUI

    //ビュー座標に変換したいオブジェクトポジション
    [SerializeField] private GameObject obj = null;

    //それぞれのスコアの値
    public enum ScoreType
    {
        high = 50,
        midle = 30,
        low = 10,
        outOfScreen = 0,
    };

    //スコア倍率(レベルに応じて値が異なる)
    private readonly float Odds_Level1 = 1.2f;
    private readonly float Odds_Level2 = 1.5f;
    private readonly float Odds_Level3 = 2.0f;

    //それぞれの判定の幅
    private const float areaWidth = 192;      //(960 / 5) 画面5等分
    private const float areaHeight = 216;     //(1080 / 5) 画面5等分


    public static int[] abc = new int[10];

    void Awake()
    {
        //lostTimeImg.enabled = false;
        imgTime = 0;
        setterObj = new List<GameObject>();
        destroyStrangeList = new List<int>();
        InitialPrevPos = targetImage.rectTransform.position;
        InitialPrevscale = targetImage.rectTransform.localScale;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        targetImage.enabled = false;
        noneStrangeFlag = true;
        noneTargetFlag = true;
        player = GameObject.FindObjectOfType<Player>();
        skillManager = GetComponent<SkillManager>();
    }

    private void Start()
    {
        //getTimeImg.enabled = false;
        //画面の中心を求める
        center = new Vector3(
            areaWidth * 2 + areaWidth * 0.5f,
            areaHeight * 2 + areaHeight * 0.5f,
            0.0f);
    }

    private void Update()
    {
        if (Shutter.isFilming)
        {
            for (int i = 0; i < gameUI.Length; i++)
            {
                gameUI[i].SetActive(false);
            }

            InitializeRawImage();
            ClickShootButton();
            Invoke(nameof(FirstMovePreview), 1f);
            //GameManager.Instance.IsPhoto = true;

            //サブカメラ撮影判定がオンだったときの判定
            for (int i = 0; i < setterObj.Count; i++)
            {
                if (setterObj[i] != null && setterObj[i].GetComponent<HeterogeneousController>().GetEnableTakePicFlag())
                {
                    Debug.Log("異質なもの撮影");
        
                    noneStrangeFlag = false;
                    //サブカメラカウントをインクリメント
                    GameManager.Instance.numSubShutter++;
                    //Debug.Log("1");
                    //スコアを加算
                    ScoreManger.Score += 10;
                    //Debug.Log("2");
                    //tempList[i]のオブジェクトの消滅フラグをオンにする
                    setterObj[i].GetComponent<HeterogeneousController>().SetTakenPicFlag(true);
                    //Debug.Log("処理完了");
                    //リストにこの配列のインデックスを追加
                    //destroyStrangeList.Add(i);
                    //アニメーション
                    Invoke("startOA", 0.2f);
                }
            }

            //Debug.Log("通ってる(1)");
            //障害物があるとき
            if (createRay())
            {
                Debug.Log("障害蟻");
                SEManager.Instance.PlayShot();
            }
            else
            {
                switch(checkScore(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position)))
                {
                    case ScoreType.low:
                        ShutterManager();
                        GameManager.Instance.numLowScore++;
                        break;
                    case ScoreType.midle:
                        ShutterManager();
                        GameManager.Instance.numMiddleScore++;
                        break;
                    case ScoreType.high:
                        ShutterManager();
                        GameManager.Instance.numHighScore++;
                        break;
                    case ScoreType.outOfScreen:
                        SEManager.Instance.PlayShot();
                        ScreenShot.noneTargetFlag = true;
                        Debug.Log("画面外");
                        break;
                }
                ////ターゲットが画面外か
                //if ( == ScoreType.outOfScreen)
                //{
                //    SEManager.Instance.PlayShot();
                //    ScreenShot.noneTargetFlag = true;
                //    Debug.Log("画面外");
                //}
                //else
                //{

                //}
            }

            //空撮り（異質なもの、ターゲットが撮影されていない）していたら
            if (noneTargetFlag && noneStrangeFlag && Shutter.isFilming)
            {
                //Debug.Log("時間を失いました");
                Debug.Log("何も撮影できてない");
                CountDownTimer.DecreaceTime();
                player.Shake(duration, magnitude);
                ShutterAnimation.NoneAnimationStart();
                TimerUI.FadeOut(false);
                SEManager.Instance.PlayPlusTimeCountSE();
                //アニメーション
                Invoke("startNA", 0.2f);
            }
            //フラグを初期化
            noneTargetFlag = true;
            noneStrangeFlag = true;

            Invoke(nameof(OnUIShutter), 0.2f);
            Shutter.isFilming = false;
        }
    }

    //撮影時の処理
    public void ShutterManager()
    {
        Transform transform = RespawTarget.GetCurrentTargetObj().transform;
        Instantiate(mimic, transform.position, transform.rotation);
        ScreenShot.noneTargetFlag = false;
        //時間を獲得
        CountDownTimer.IncreaceTime();
        Debug.Log("時間を獲得しました");
        //ターゲットが撮影された
        SetPhotographTargetFlag(false);
        //障害物がないときの処理
        Debug.Log("撮影した");
        //スコア加算
        ScoreManger.Score += FinalScorePoint(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position));
        ScoreManger.ShotMainTarget = true;
        TargetManager.IsSpawn = true;
        //対象を撮影した回数をインクリメント
        GameManager.Instance.numTargetShutter++;
        //SE
        SEManager.Instance.PlayTargetShot();
        //ターゲットリスポーン
        RespawTarget.RespawnTarget();
        //アニメーション開始
        Invoke("startTA", 0.2f);
        TimerUI.FadeOut(true);
        SEManager.Instance.PlayMinusTimeCountSE();
    }

    //撮影する瞬間非表示にされたUIを表示する関数（Invokeで呼ぶ）
    public void OnUIShutter()
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(true);
        }
    }


    //特定のImageを表示、非表示する関数
    public void UIManager_Image(Image img, bool isActive)
    {
        img.enabled = isActive;
    }

    //特定のTextを表示、非表示する関数
    public void UIManager_Text(Text text, bool isActive)
    {
        text.enabled = isActive;
    }

    private void InitializeRawImage()
    {
        targetImage.texture = null;
        targetImage.enabled = false;
        targetImage.rectTransform.position = InitialPrevPos;
        targetImage.rectTransform.localScale = InitialPrevscale;
    }

    private string GetScreenShotPath()
    {
        //string path = "Assets/Pictures/" + timeStamp + ".png";
        string path = GameManager.Instance.GetPicturesFilePath() + timeStamp + ".png";

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




        //================================================================================================================================================
        //
        //   もし。テクスチャが縮められている状態だったら下のコメント化している部分（「 / 2」）のところをはずしてみて）
        //
        //================================================================================================================================================




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

    public void ClickShootButton()
    {
        StartCoroutine(CreateScreenShot());
    }

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

    private void FirstMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * firstScale, 0.5f);
        Invoke(nameof(SecondMovePreview), 0.5f);
    }

    private void SecondMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * secondScale, 0.5f);
        targetImage.rectTransform.DOMove(point1.rectTransform.position, 0.5f);
        Invoke(nameof(SlideMovePreview), 1f);
    }

    private void SlideMovePreview()
    {
        targetImage.transform.DOMoveX(point2.rectTransform.position.x, 0.3f);
    }
    void startOA()
    {
        ShutterAnimation.OtherAnimationStart();
    }
    void startNA()
    {
        ShutterAnimation.NoneAnimationStart();
    }

    public void SetList(List<GameObject> list) { setterObj = list; }

    public void SetDestroyList(List<int> list) { destroyStrangeList = list; }

    public List<int> GetDestroyList() { return destroyStrangeList; }

    public void SetPhotographTargetFlag(bool flag) { noneTargetFlag = flag; }





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
        Vector3 diff = RespawTarget.GetCurrentTargetObj().transform.position - player.transform.position;

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
    void startTA()
    {
        ShutterAnimation.TargetAnimationStart();
    }
}
