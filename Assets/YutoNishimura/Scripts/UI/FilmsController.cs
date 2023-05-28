using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//ゲーム中に撮影した写真を表示するクラス
public class FilmsController : MonoBehaviour
{
    [Header("RawImage(NoImageみたいな画像が貼ってあるRawImage)を入れてください")]
    [SerializeField] private RawImage pictures;           //RawImage
    [Header("何秒間表示するか")]
    [SerializeField] private float intervalChangeTime = 0.5f;
    [Header("フェード時間")]
    private float fadeTime = 0.5f;    //フェードしたい時間（単位は秒）
    private string[] picturesFilepathes;

    //フェード用Imageの透明度
    private static float alpha = 0.0f;

    //フェードインアウトのフラグ
    private bool isFadeIn = false;
    private bool isFadeOut = false;


    private float enabledTime = 0;

    private List<Texture> images;

    private int pictureIndex;    //今何番目の写真を表示しているか

    // Start is called before the first frame update
    void Start()
    {
        images = new List<Texture>();
        //インデックスは０番目から
        pictureIndex = 0;
        //アルファ値は０
        alpha = 0;
        LoadPictures();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeOut)
        {
            //経過時間から透明度計算
            alpha += Time.deltaTime / fadeTime;

            //フェードアウト終了判定
            if (alpha >= 1.0f)
            {
                alpha = 1.0f;

                enabledTime += Time.deltaTime;

                if (enabledTime > intervalChangeTime)
                {
                    isFadeOut = false;
                    isFadeIn = true;
                    enabledTime = 0;
                }
            }
        }
        else if(isFadeIn)
        {
            //経過時間から透明度計算
            alpha -= Time.deltaTime / fadeTime;

            //フェードアウト終了判定
            if (alpha <= 0)
            {
                alpha = 0f;
                isFadeOut = true;
                isFadeIn = false;

                //ここで写真のデータを切り替える
                pictureIndex = (pictureIndex + 1) % picturesFilepathes.Length;
                pictures.texture = images[pictureIndex];
            }
        }

        //フェード用Imageの色・透明度設定
        pictures.color = new Color(pictures.color.r, pictures.color.g, pictures.color.b, alpha);
    }

    //撮った写真をロード
    public void LoadPictures()
    {
        string directoryPath = GameManager.Instance.GetDirectryPath();
        //もしディレクトリが存在していたら
        if (Directory.Exists(directoryPath))
        {
            //写真フォルダを取得
            picturesFilepathes = Directory.GetFiles(directoryPath);

            if (picturesFilepathes.Length != 0)
            {
                for (int i = 0; i < picturesFilepathes.Length; i++)
                {
                    if (!String.IsNullOrEmpty(picturesFilepathes[i]))
                    {
                        //テクスチャ情報を読み込み
                        byte[] image = File.ReadAllBytes(picturesFilepathes[i]);

                        Texture2D tex = new Texture2D(0, 0);
                        tex.LoadImage(image);

                        // 配列にデータを挿入
                        images.Add(tex);
                    }
                }

                //1番目の写真を表示したい
                pictures.texture = images[pictureIndex];
                isFadeOut = true;
            }
        }
        else
        {
            alpha = 1;
        }
    }
}
