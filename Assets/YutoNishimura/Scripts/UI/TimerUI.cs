using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//追加５秒、マイナス５秒の時にイメージを表示する
//イメージはフェードインする
public class TimerUI : MonoBehaviour
{
    //フェード用のCanvasとImage
    private static Canvas fadeCanvas;
    private static Image fadeImage;

    //フェード用Imageの透明度
    private static float alpha = 0.0f;

    //フェードインアウトのフラグ
    public static bool isFadeIn = false;
    public static bool isFadeOut = false;

    //フェードしたい時間（単位は秒）
    private static float fadeTime = 0.2f;

    private static float enabledTime = 0;

    //遷移先のシーン番号
    private static int nextScene = 1;

    //フェード用のCanvasとImage生成
    public static void Init()
    {

    }

    //フェードアウト開始
    public static void FadeOut(bool getPoints)
    {
        if (getPoints)
        {
            fadeImage = GameObject.Find("GetPoint").GetComponent<Image>();
        }
        else
        {
            fadeImage = GameObject.Find("LostPoint").GetComponent<Image>();
        }
        fadeImage.enabled = true;
        alpha = 0;
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
        isFadeOut = true;
    }

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

                if(enabledTime > 1.0f)
                {
                    isFadeOut = false;
                    enabledTime = 0;
                    fadeImage.enabled = false;
                }
            }

            //フェード用Imageの色・透明度設定
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
        }
    }
}
