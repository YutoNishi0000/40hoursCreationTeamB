using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    private AudioSource audioSource_BGM;
    private AudioSource[] audioSource_SE;

    private void Start()
    {
        audioSource_BGM = BGMManager.Instance.GetBGMAudioSource();
        audioSource_SE = SEManager.Instance.GetSEAudioSources();
    }

    /// <summary>
    /// スライドバー値の変更イベント（BGM用） 
    /// </summary>
    /// <param name="newSliderValue">スライドバーの値(自動的に引数に値が入る)</param>
    public void SoundSliderOnValueChange_BGM(float sliderValue)
    {
        audioSource_BGM.volume = sliderValue;
    }

    /// <summary>
    /// スライドバー値の変更イベント（SE用） 
    /// </summary>
    /// <param name="newSliderValue">スライドバーの値(自動的に引数に値が入る)</param>
    public void SoundSliderOnValueChange_SE(float sliderValue)
    {
        for(int i = 0; i < audioSource_SE.Length; i++)
        {
            audioSource_SE[i].volume = sliderValue;
        }
    }

    public void MouseSensitivityOnValueChange(float sensitivity)
    {
        Config.mouseHorizon = sensitivity;
        Config.mouseVertical = sensitivity;
    }
}
