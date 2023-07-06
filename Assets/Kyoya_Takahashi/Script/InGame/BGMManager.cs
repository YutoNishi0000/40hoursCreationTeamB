using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : SingletonMonoBehaviour<BGMManager>
{
    //0:タイトルからステージ選択まで
    //1:インゲーム中
    //2:リザルト画面
    public enum BGMType
    {
        OutGame,
        Ingame,
        ResultGame
    }

    public AudioClip[] BGM = new AudioClip[3];

    public AudioSource audioSource = null;
    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void BGMAdministrator(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case (int)GameManager.GameState.MainGame:
                PlayBGM(BGMType.Ingame);
                break;
            case (int)GameManager.GameState.Result:
                PlayBGM(BGMType.ResultGame);
                break;
            case (int)GameManager.GameState.Operator:
                StopBGM();
                break;
            default:
                if (!Instance.audioSource.isPlaying || Instance.audioSource.clip != Instance.BGM[(int)BGMType.OutGame])
                {
                    PlayBGM(BGMType.OutGame);
                }
                break;
        }
    }

    public void PlayBGM(BGMType type)
    {
        audioSource.Stop();
        audioSource.clip = BGM[(int)type];
        audioSource.Play();
        audioSource.loop = true;
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}
