using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class ScreenShotProt : Human
{
    Camera cam;
    GameObject targetImage;
    string screenShotPath;
    List<string> filePathes;
    string timeStamp;
    public RawImage preview;

    [SerializeField]
    private Image _image = null;

    public Image prevPos;
    public Image prevPos2;
    public Vector3 InitialPrevPos;
    public Vector3 InitialPrevscale;
    public Text SucceededShutter;
    public Text FailedShutter;

    private int numShutter;           //サブカメラでシャッターを切った回数
    private List<GameObject> setterObj;


    void Awake()
    {
        filePathes = new List<string>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        targetImage = GameObject.Find("RawImage");
        preview.enabled = false;
        _image.enabled = false;
        SucceededShutter.enabled = false;
        FailedShutter.enabled = false;
        InitialPrevPos = preview.rectTransform.position;
        InitialPrevscale = preview.rectTransform.localScale;
        numShutter = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            preview.enabled = true;
            //preview.transform.position = Vector3.zero;
            GameManager.Instance.IsPhoto = true;
            Shutter();
            //OffPreview();
            //todayTask.TaskCompletion(1);
            ClickShootButton();
            ShowSSImage();
            //Invoke(nameof(MovePreview), 1f);
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            preview.enabled = true;
            //OffPreview();
            //StartCoroutine(nameof(HiddonText), SucceededShutter);
            //todayTask.TaskCompletion(1);
            ClickShootButton();
            ShowSSImage();
            //Invoke(nameof(MovePreview), 1f);

            Debug.Log(setterObj.Count);

            for (int i = 0; i < setterObj.Count; i++)
            {
                if (setterObj[i] != null && setterObj[i].GetComponent<HeterogeneousController>().GetEnableTakePicFlag())
                {
                    //サブカメラカウントをインクリメント
                    GameManager.Instance.numSubShutter++;
                    Debug.Log("1");
                    //スコアを加算
                    ScoreManger.Score += 10;
                    Debug.Log("2");
                    //tempList[i]のオブジェクトの消滅フラグをオンにする
                    setterObj[i].GetComponent<HeterogeneousController>().SetTakenPicFlag(true);
                    Debug.Log("処理完了");
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            //フォルダ内の写真を全て削除
            ClearCash(filePathes);
        }
    }

    public void SetList(List<GameObject> list) { setterObj = list; }

    //撮影関数
    private void Shutter()
    {
        preview.enabled = true;
        OffPreview();
        StartCoroutine(nameof(HiddonText), SucceededShutter);
        //todayTask.TaskCompletion(1);
        ClickShootButton();
        ShowSSImage();
        FadeIn(0.5f, _image);
        Invoke(nameof(MovePreview), 1f);
    }

    void MovePreview()
    {
        preview.rectTransform.DOScale(transform.localScale / 5, 0.5f);
        preview.rectTransform.DOMove(prevPos.rectTransform.position, 0.5f);
        Invoke(nameof(SlideMovePreview), 1f);
    }

    void SlideMovePreview()
    {
        preview.transform.DOMoveX(prevPos2.rectTransform.position.x, 0.3f);
    }

    /// <summary>
    /// テキストを非表示にします
    /// </summary>
    /// <param name="text">非表示にしたいテキスト</param>
    /// <param name="timne">何秒後に非表示にするか</param>
    IEnumerator HiddonText(Text text)
    {
        text.enabled = true;

        yield return new WaitForSeconds(2);

        text.enabled = false;
    }

    void OffPreview()
    {
        //FadeIn(0.5f, preview);
        preview.enabled = false;
        preview.rectTransform.position = InitialPrevPos;
        preview.rectTransform.localScale = InitialPrevscale;
    }

    private string GetScreenShotPath()
    {
        //Picturesフォルダの中に撮った写真を置くようにする
        string path = "Assets/Pictures/" + timeStamp + ".png";

        return path;
    }

    private IEnumerator CreateScreenShot()
    {
        //テクスチャの名前を現在時刻に設定
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
        //screenShotPath = ;

        // ファイルとして保存するならFile.WriteAllBytes()を実行
        File.WriteAllBytes(GetScreenShotPath(), pngData);

        filePathes.Add(GetScreenShotPath());

        cam.targetTexture = null;
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

    public void ClickShootButton()
    {
        //SoundManager.Instance.PlayCameraSE();
        StartCoroutine(CreateScreenShot());
    }

    public void ShowSSImage()
    {
        if (!String.IsNullOrEmpty(screenShotPath))
        {
            byte[] image = File.ReadAllBytes(GetScreenShotPath());

            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(image);

            // NGUI の UITexture に表示
            RawImage target = targetImage.GetComponent<RawImage>();
            target.texture = tex;
        }
    }

    private void ClearCash(List<string> pathes)
    {
        //パスが入ったリストを動的にコピー
        var numPathes = pathes;

        //撮った写真は全て削除
        for(int i = 0; i  < numPathes.Count; i++)
        {
            File.Delete(pathes[i]);
        }

        //パスが入ったリストは中身を空にする
        pathes.Clear();
    }

    /// <summary>
    /// 規定値に戻す
    /// </summary>
    private void Reset()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>
    /// フェードインする(オーバーロード関数)
    /// </summary>
    public void FadeIn(float duration, RawImage image, Action on_completed = null)
    {
        image.enabled = true;
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, image, on_completed, true));
    }

    /// <summary>
    /// フェードインする
    /// </summary>
    public void FadeIn(float duration, Image image, Action on_completed = null)
    {
        image.enabled = true;
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, image, on_completed, true));
    }

    /// <summary>
    /// フェードアウトする
    /// </summary>
    public void FadeOut(float duration, Image image, Action on_completed = null)
    {
        image.enabled = true;
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, image, on_completed));
    }

    /// <summary>
    /// フェードアウトする(オーバーロード関数)
    /// </summary>
    public void FadeOut(float duration, RawImage image, Action on_completed = null)
    {
        image.enabled = true;
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, image, on_completed));
    }

    /// <summary>
    /// 時間経過でアルファ値を「0」から「1」に変更
    /// </summary>
    private IEnumerator ChangeAlphaValueFrom0To1OverTime(
        float duration,
        Image image,
        Action on_completed,
        bool is_reversing = false
    )
    {
        if (!is_reversing) image.enabled = true;

        var elapsed_time = 0.0f;
        var color = image.color;

        while (elapsed_time < duration)
        {
            var elapsed_rate = Mathf.Min(elapsed_time / duration, 1.0f);
            color.a = is_reversing ? 1.0f - elapsed_rate : elapsed_rate;
            image.color = color;

            yield return null;
            elapsed_time += Time.deltaTime;
        }

        if (is_reversing) image.enabled = false;
        if (on_completed != null) on_completed();
    }

    /// <summary>
    /// 時間経過でアルファ値を「0」から「1」に変更(オーバーロード関数)
    /// </summary>
    private IEnumerator ChangeAlphaValueFrom0To1OverTime(
        float duration,
        RawImage image,
        Action on_completed,
        bool is_reversing = false
    )
    {
        if (!is_reversing) image.enabled = true;

        var elapsed_time = 0.0f;
        var color = image.color;

        while (elapsed_time < duration)
        {
            var elapsed_rate = Mathf.Min(elapsed_time / duration, 1.0f);
            color.a = is_reversing ? 1.0f - elapsed_rate : elapsed_rate;
            image.color = color;

            yield return null;
            elapsed_time += Time.deltaTime;
        }

        if (is_reversing) image.enabled = false;
        if (on_completed != null) on_completed();
    }
}
