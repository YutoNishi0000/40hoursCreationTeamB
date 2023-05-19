using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class ScreenShot : MonoBehaviour
{
    private Camera cam;                                //プレイヤーのカメラ
    [SerializeField] private RawImage targetImage;     //テクスチャを表示するための物
    [SerializeField] private Image point1;             //スクショした画像の１番目の移動先
    [SerializeField] private Image point2;             //スクショした画像の２番目の移動先
    [SerializeField] private Image point3;             //スクショした画像の３番目の移動先
    private string screenShotPath;                     //スクリーンショットして生成されたテクスチャのファイルパス
    private string timeStamp;                          //現在時刻を表すためのもの
    private const float firstScale = 0.8f;             //一回目移動するときにどれだけRawImnageが縮小されるか（何倍の大きさになるか）
    private const float secondScale = 0.2f;            //二回目縮小するときにどれだけRawImageが縮小されるか（何倍の大きさになるか）
    private RawImage initialTargetImage;               //RawImageの初期値

    void Awake()
    {
        initialTargetImage = targetImage;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            InitializeRawImage();
            ClickShootButton();
        }
    }

    private void InitializeRawImage()
    {
        targetImage = initialTargetImage;
    }

    private string GetScreenShotPath()
    {
        string path = "Assets/Pictures/" + timeStamp + ".png";

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
        }
    }
}
