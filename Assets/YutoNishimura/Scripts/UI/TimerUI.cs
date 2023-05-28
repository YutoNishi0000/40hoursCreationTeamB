using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//追加５秒、マイナス５秒の時にイメージを表示する
//イメージはフェードインする
public class TimerUI : MonoBehaviour
{
    private IEnumerator ChangeAlphaValueFrom0To1OverTime(
    float duration,
    Action on_completed,
    Image img,
    bool is_reversing = false
)
    {
        img.enabled = true;

        var elapsed_time = 0.0f;
        var color = img.color;

        while (elapsed_time < duration)
        {
            var elapsed_rate = Mathf.Min(elapsed_time / duration, 1.0f);
            color.a = is_reversing ? 1.0f - elapsed_rate : elapsed_rate;
            img.color = color;

            yield return null;
            elapsed_time += Time.deltaTime;
        }

        img.enabled = false;
        if (on_completed != null) on_completed();
    }

    public void FadeIn( float duration, Image image, Action on_completed = null ) 
    {
        StartCoroutine( ChangeAlphaValueFrom0To1OverTime( duration, on_completed, image, true ) );
    }

    public void FadeOut( float duration, Image image, Action on_completed = null ) 
    {
        StartCoroutine( ChangeAlphaValueFrom0To1OverTime( duration, on_completed, image) );
    }


    //private static Image fadeImage;
    ////private static Image anotherImage;       //モードがハードだった時に追加で表示したいイメージ
    //[SerializeField] private static Image plusImg;
    //[SerializeField] private static Image minusImg;

    ////フェード用Imageの透明度
    //private static float alpha = 0.0f;

    ////フェードインアウトのフラグ
    //public static bool isFadeIn = false;
    //public static bool isFadeOut = false;

    ////フェードしたい時間（単位は秒）
    //private static float fadeTime = 0.5f;

    //private static float enabledTime = 0;

    //private void Start()
    //{
    //    fadeImage = null;
    //}

    ////フェード用のCanvasとImage生成
    //public static void Init()
    //{

    //}

    ////フェードアウト開始
    //public static void FadeOut(bool getPoints)
    //{
    //    if (getPoints)
    //    {
    //        while (!fadeImage)
    //        {
    //            fadeImage = GameObject.Find("GetPoint").GetComponent<Image>();
    //        }
    //    }
    //    else
    //    {
    //        while (!fadeImage)
    //        {
    //            fadeImage = GameObject.Find("LostPoint_1").GetComponent<Image>();
    //        }
    //        //anotherImage = GameObject.Find("LostPoint_2").GetComponent<Image>();
    //    }

    //    fadeImage.enabled = true;
    //    alpha = 0;
    //    fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
    //    //anotherImage.color = new Color(anotherImage.color.r, anotherImage.color.g, anotherImage.color.b, alpha);
    //    isFadeOut = true;
    //}

    //void Update()
    //{
    //    if (isFadeOut)
    //    {
    //        //経過時間から透明度計算
    //        alpha += Time.deltaTime / fadeTime;

    //        //フェードアウト終了判定
    //        if (alpha >= 1.0f)
    //        {
    //            alpha = 1.0f;

    //            enabledTime += Time.deltaTime;

    //            if(enabledTime > 1.0f)
    //            {
    //                isFadeOut = false;
    //                enabledTime = 0;
    //                fadeImage.enabled = false;
    //            }
    //        }

    //        //フェード用Imageの色・透明度設定
    //        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);

    //        //if(GameManager.Instance.GetGameMode() == GameManager.GameMode.Hard)
    //        //{
    //        //    anotherImage.color = new Color(anotherImage.color.r, anotherImage.color.g, anotherImage.color.b, alpha);
    //        //}
    //    }
    //}
}
