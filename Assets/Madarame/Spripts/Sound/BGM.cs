using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public enum BGMType
{
    InGame, OutGame, GameClear,GameOver
}
public class BGM : MonoBehaviour
{
    private static BGM instance;
    public static BGM Instance
    {
        get => instance;
    }

    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] BGMType bgmType;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SetBGM(true, bgmType);
    }
    private void SetBGM(bool isPlaying, BGMType bgmType)
    {
        audioSource.clip = audioClips[(int)bgmType];

        if (isPlaying)
        {
            PlayBGM();
        }
    }
    public void SetOutGameBGM(bool isPlaying)
    {
        SetBGM(isPlaying, BGMType.OutGame);
    }
    public void SetInGameBGM(bool isPlaying)
    {
        SetBGM(isPlaying, BGMType.InGame);
    }
    public void SetGameClearBGM(bool isPlaying)
    {
        SetBGM(isPlaying, BGMType.GameClear);
    }
    public void SetGameOverBGM(bool isPlaying)
    {
        SetBGM(isPlaying, BGMType.GameOver);
    }

    public void PlayBGM()
    {
        audioSource.Play();
    }
}
