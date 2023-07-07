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
    /// �X���C�h�o�[�l�̕ύX�C�x���g�iBGM�p�j 
    /// </summary>
    /// <param name="newSliderValue">�X���C�h�o�[�̒l(�����I�Ɉ����ɒl������)</param>
    public void SoundSliderOnValueChange_BGM(float sliderValue)
    {
        audioSource_BGM.volume = sliderValue;
    }

    /// <summary>
    /// �X���C�h�o�[�l�̕ύX�C�x���g�iSE�p�j 
    /// </summary>
    /// <param name="newSliderValue">�X���C�h�o�[�̒l(�����I�Ɉ����ɒl������)</param>
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
