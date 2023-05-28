using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : SingletonMonoBehaviour<BGMManager>
{
    //0:タイトルからステージ選択まで
    //1:インゲーム中
    //2:リザルト画面
    [SerializeField]AudioClip[] BGM = new AudioClip[3];
    private const int OutGame = 0;
    private const int InGame = 1;
    private const int ResultGame = 2;

    public AudioSource audioSource = null;
    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
    public void PlayOutGameBGM()
    {
        audioSource.Stop();
        audioSource.clip = BGM[OutGame];
        audioSource.Play();
        audioSource.loop = true;
    }
    public void PlayInGameBGM()
    {
        Debug.Log("インゲーム");
        audioSource.Stop();
        audioSource.clip = BGM[InGame];
        audioSource.Play();
        audioSource.loop = true;
    }
    
    public void PlayResultBGM()
    {
        audioSource.Stop();
        audioSource.clip = BGM[ResultGame];
        audioSource.Play();
        audioSource.loop = true;
    }
}
