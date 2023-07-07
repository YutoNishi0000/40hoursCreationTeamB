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
}
