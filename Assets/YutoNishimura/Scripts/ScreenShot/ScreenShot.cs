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
using System.Threading;

[RequireComponent(typeof(TimerUI))]
public class ScreenShot : UniTaskController
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
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Image raticle;            //レティクルUI
    //[SerializeField] private GameObject animationManager;

    //内部処理で使うもの
    private Camera cam;                                //プレイヤーのカメラ
    private string screenShotPath;                     //スクリーンショットして生成されたテクスチャのファイルパス
    private string timeStamp;                          //現在時刻を表すためのもの
    private static JudgeSubTarget.Judge judgeSubTargetFlag;
    private bool judgeTargetFlag;
    private Vector3 InitialPrevPos;                    //RawImageの初期位置
    private Vector3 InitialPrevscale;                  //RawImageの初期スケール
    private Vector3 center = Vector3.zero;             //画面の中心
    internal List<GameObject> setterObj;          //毎フレーム送られてくる異質なものの情報を取得するためのもの
    private SkillManager skillManager;
    private TimerUI fadeManager;
    private JudgeScreenShot judge;

    //シャッターアニメーションの種類
    private enum ShutterAnimationType
    {
        None,
        Target,
        Other
    }

    //それぞれの判定の幅
    private const float areaWidth = 192;      //(960 / 5) 画面5等分
    private const float areaHeight = 216;     //(1080 / 5) 画面5等分

    private void Start()
    {
        judge = new JudgeScreenShot(particle);
        judgeSubTargetFlag = new JudgeSubTarget.Judge();
        judgeTargetFlag = false;
        setterObj = new List<GameObject>();
        InitialPrevPos = targetImage.rectTransform.position;
        InitialPrevscale = targetImage.rectTransform.localScale;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        targetImage.enabled = false;
        skillManager = GetComponent<SkillManager>();
        fadeManager = GetComponent<TimerUI>();
        //画面の中心を求める
        center = new Vector3(areaWidth * 2 + areaWidth * 0.5f, areaHeight * 2 + areaHeight * 0.5f, 0.0f);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlayGame)
        {
            return;
        }

        judgeSubTargetFlag = judge.judgeSubTarget.ShutterSubTargets(cam, player, setterObj, Config.subTargetJudgeLength);

        if (Shutter.isFilming)
        {
            InitializeRawImage();
            ClickShootButton();
            Invoke(nameof(FirstMovePreview), Config.movePrevTimeFirst);

            judgeTargetFlag = judge.judgeTarget.ShutterTarget(player, mimic, cam, RespawTarget.GetCurrentTargetObj().transform.position, center, areaWidth, areaHeight, Config.raiseScore);

            ShutterAnimationController(Config.delayTimeShutterAnimation, judgeTargetFlag, judgeSubTargetFlag.shutterFlag);

            //フラグを初期化
            judgeTargetFlag = false;
            //judgeSubTargetFlag = false;

            Shutter.isFilming = false;
        }
    }

    #region 撮影系

    //空撮りしたときの処理
    private void ShutterNone()
    {
        CountDownTimer.DecreaceTime();
        //player.GetComponent<Player>().Shake(duration, magnitude);
        SEManager.Instance.PlayMinusTimeCountSE();
        fadeManager.FadeOut(Config.fadeOutSpeed, minusCount1);

        //もし難易度がハードだったら
        if (GameManager.Instance.GetGameMode() == GameManager.GameMode.Hard)
        {
            //もう５秒制限時間を減らす
            CountDownTimer.DecreaceTime();
            fadeManager.FadeOut(Config.fadeOutSpeed, minusCount2);
        }
    }

    //空撮りしたときの処理
    private void ShutterTraget()
    {
        CountDownTimer.IncreaceTime();
        SEManager.Instance.PlayPlusTimeCountSE();
        fadeManager.FadeOut(Config.fadeOutSpeed, plusCount);
    }

    //シャッターアニメーションを管理する関数
    private void ShutterAnimationController(float invokeTime, bool targetFlag, bool subTargetFlag)
    {
        //異質な物だけ撮影した場合
        if (subTargetFlag && !targetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Other);
        }
        //ターゲットだけ撮影した場合
        else if (!subTargetFlag && targetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Target);
            ShutterTraget();
        }
        //どちらも撮影した場合
        else if (subTargetFlag && targetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Target);
        }
        //空撮りだった場合
        else
        {
            ShutterAnimationmanager(ShutterAnimationType.None);
            ShutterNone();
        }
    }

    //撮った写真のファイルパスを取得
    private string GetScreenShotPath()
    {
        string directoryPath = GameManager.Instance.GetDirectryPath();

        string path = GameManager.Instance.GetPicturesFilePath(directoryPath) + timeStamp + ".png";

        return path;
    }

    /// <summary>
    /// 撮影を実際に行う関数
    /// </summary>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns></returns>
    private async UniTask CreateScreenShot(CancellationToken cancelToken)
    {
        DateTime date = DateTime.Now;
        timeStamp = date.ToString("yyyy-MM-dd-HH-mm-ss-fff");

        PostEffectController.SetPostEffectFlag(false);

        ////任意のフレームの描画処理が終わるまで待つ
        await UniTask.DelayFrame(1, PlayerLoopTiming.Update, cancelToken);

        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture rt = new RenderTexture(screenShot.width, screenShot.height, 24);
        RenderTexture prev = cam.targetTexture;
        cam.targetTexture = rt;
        cam.Render();
        cam.targetTexture = prev;
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply();

        //screenShot = ResizeTexture(screenShot, 960, 1080);

        byte[] pngData = screenShot.EncodeToPNG();
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
        //自身が破棄されるときにUnitaskを中止するためのキャンセルトークンを取得
        var token = this.GetCancellationTokenOnDestroy();
        CreateScreenShot(token).Forget();
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

    Texture2D ResizeTexture(Texture2D src, int dst_w, int dst_h)
    {
        Texture2D dst = new Texture2D(dst_w, dst_h, src.format, false);

        float inv_w = 1f / dst_w;
        float inv_h = 1f / dst_h;

        for (int y = 0; y < dst_h; ++y)
        {
            for (int x = 0; x < dst_w; ++x)
            {
                dst.SetPixel(x, y, src.GetPixelBilinear((float)x * inv_w, (float)y * inv_h));
            }
        }
        return dst;
    }

    #endregion

    #region UI系

    private void InitializeRawImage()
    {
        //プレビューを初期化する
        targetImage.texture = null;
        targetImage.enabled = false;
        targetImage.rectTransform.position = InitialPrevPos;
        targetImage.rectTransform.localScale = InitialPrevscale;
    }

    #endregion

    #region プレビューの動き
    private void FirstMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * Config.reduceScaleFirst, Config.changePrevTransformFirst);
        Invoke(nameof(SecondMovePreview), Config.movePrevTimeSecond);
    }

    private void SecondMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * Config.reduceScaleSecond, Config.changePrevTransformSecond);
        targetImage.rectTransform.DOMove(point1.rectTransform.position, Config.changePrevTransformSecond);
        Invoke(nameof(SlideMovePreview), Config.movePrevTimeThird);
    }

    private void SlideMovePreview()
    {
        targetImage.transform.DOMoveX(point2.rectTransform.position.x, Config.changePrevTransformThird);
    }

    #endregion

    #region シャッターアニメーション

    private void ShutterAnimationmanager(ShutterAnimationType type)
    {
        switch(type)
        {
            case ShutterAnimationType.None:
                GetComponent<ShutterAnimation>().StartAnimation(GameManager.ShutterAnimationState.None);
                break;
            case ShutterAnimationType.Target:
                GetComponent<ShutterAnimation>().StartAnimation(GameManager.ShutterAnimationState.Target);
                break;
            case ShutterAnimationType.Other:
                GetComponent<ShutterAnimation>().StartAnimation(GameManager.ShutterAnimationState.Other);
                break;
        }
    }

    #endregion

    #region ゲッター、セッター

    public void SetList(List<GameObject> list) { setterObj = list; }

    public static bool GetJudgeSubTargetFlag() { return judgeSubTargetFlag.enableShutterFlag; }

    #endregion
}
