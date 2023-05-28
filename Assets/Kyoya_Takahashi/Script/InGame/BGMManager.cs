using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : SingletonMonoBehaviour<BGMManager>
{
    //0:�^�C�g������X�e�[�W�I���܂�
    //1:�C���Q�[����
    //2:���U���g���
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
        Debug.Log("�C���Q�[��");
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
