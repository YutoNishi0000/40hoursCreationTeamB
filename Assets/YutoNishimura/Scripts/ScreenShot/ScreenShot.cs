using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenShot : Human
{
    Camera cam;
    GameObject canvas;
    GameObject targetImage;
    string screenShotPath;
    string timeStamp;
    public RawImage preview;

    [SerializeField]
    private Image _image = null;

    private PlayerStateController playerState;
    private ChangeCameraAngle _changeCamera;

    void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas = GameObject.Find("Canvas");
        targetImage = GameObject.Find("RawImage");
        preview.enabled = false;
        _image.enabled = false;
        playerState = FindObjectOfType<PlayerStateController>();
        _changeCamera= FindObjectOfType<ChangeCameraAngle>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerState.GetPlayerState() == PlayerStateController.PlayerState.Voyeurism)
        {
            ClickShootButton();
            FadeIn(0.5f);
            preview.enabled = true;
            Invoke(nameof(OffPreview), 1f);
        }
    }

    void OffPreview()
    {
        preview.enabled = false;
        _changeCamera.ExitVoyeurism();
    }

    private string GetScreenShotPath()
    {
        string path = "";
        // プロジェクトファイル直下に作成
        path = timeStamp + ".png";
        //		path = Application.persistentDataPath+timeStamp + ".png";
        return path;
    }

    // UIを消したい場合はcanvasを非アクティブにする
    private void UIStateChange()
    {
        canvas.SetActive(!canvas.activeSelf);
    }

    private IEnumerator CreateScreenShot()
    {
        UIStateChange();
        DateTime date = DateTime.Now;
        timeStamp = date.ToString("yyyy-MM-dd-HH-mm-ss-fff");
        // レンダリング完了まで待機
        yield return new WaitForEndOfFrame();

        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cam.targetTexture = renderTexture;

        Texture2D texture = new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        texture.Apply();

        // 保存する画像のサイズを変えるならResizeTexture()を実行
        //		texture = ResizeTexture(texture,320,240);

        byte[] pngData = texture.EncodeToPNG();
        screenShotPath = GetScreenShotPath();

        // ファイルとして保存するならFile.WriteAllBytes()を実行
        File.WriteAllBytes(screenShotPath, pngData);

        cam.targetTexture = null;

        Debug.Log("Done!");
        UIStateChange();

        ShowSSImage();
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
        }
    }

    /// <summary>
    /// 規定値に戻す
    /// </summary>
    private void Reset()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>
    /// フェードインする
    /// </summary>
    public void FadeIn(float duration, Action on_completed = null)
    {
        _image.enabled = true;
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, on_completed, true));
    }

    /// <summary>
    /// フェードアウトする
    /// </summary>
    public void FadeOut(float duration, Action on_completed = null)
    {
        _image.enabled = true;
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, on_completed));
    }

    /// <summary>
    /// 時間経過でアルファ値を「0」から「1」に変更
    /// </summary>
    private IEnumerator ChangeAlphaValueFrom0To1OverTime(
        float duration,
        Action on_completed,
        bool is_reversing = false
    )
    {
        if (!is_reversing) _image.enabled = true;

        var elapsed_time = 0.0f;
        var color = _image.color;

        while (elapsed_time < duration)
        {
            var elapsed_rate = Mathf.Min(elapsed_time / duration, 1.0f);
            color.a = is_reversing ? 1.0f - elapsed_rate : elapsed_rate;
            _image.color = color;

            yield return null;
            elapsed_time += Time.deltaTime;
        }

        if (is_reversing) _image.enabled = false;
        if (on_completed != null) on_completed();
    }
}
