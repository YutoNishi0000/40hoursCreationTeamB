using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : SingletonMonoBehaviour<BGMManager>
{
    //0:�^�C�g������X�e�[�W�I���܂�
    //1:�C���Q�[����
    //2:���U���g���
    public enum BGMTyoe
    {
        OutGame,
        Ingame,
        ResultGame
    }

    public AudioClip[] BGM = new AudioClip[3];
    private bool playBGMFlag;         //����BGM���Đ����Ă��邩

    public AudioSource audioSource = null;
    private void Start()
    {
        playBGMFlag = false;
        audioSource = this.GetComponent<AudioSource>();
    }
    public void PlayOutGameBGM()
    {
        audioSource.Stop();
        audioSource.clip = BGM[(int)BGMTyoe.OutGame];
        audioSource.Play();
        audioSource.loop = true;
        playBGMFlag = true;
    }
    public void PlayInGameBGM()
    {
        Debug.Log("�C���Q�[��");
        audioSource.Stop();
        audioSource.clip = BGM[(int)BGMTyoe.Ingame];
        audioSource.Play();
        audioSource.loop = true;
        playBGMFlag = true;
    }
    
    public void PlayResultBGM()
    {
        audioSource.Stop();
        audioSource.clip = BGM[(int)BGMTyoe.ResultGame];
        audioSource.Play();
        audioSource.loop = true;
        playBGMFlag = true;
    }

    public void StopBGM()
    {
        audioSource.Stop();
        playBGMFlag = false;
    }

    public bool GetPlayBGMFLag() { return playBGMFlag; }

    public void SetPlayBGMFLag(bool flag) { playBGMFlag = true; }
}
