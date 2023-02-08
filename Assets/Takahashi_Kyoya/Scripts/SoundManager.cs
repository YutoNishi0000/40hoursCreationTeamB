using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private AudioSource audioSource = null;
    //SE
    [SerializeField] AudioClip cameraSE;
    [SerializeField] AudioClip goodSE;
    [SerializeField] AudioClip heartBeatSE;
    [SerializeField] AudioClip nextTextSE;
    [SerializeField] AudioClip screamSE;
    [SerializeField] AudioClip badSE;
    [SerializeField] AudioClip gameoverSE;
    //BGM
    [SerializeField] AudioClip gameClearBGM;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCameraSE()
    {
        audioSource.PlayOneShot(cameraSE);
    }
    public void PlayGoodSE()
    {
        audioSource.PlayOneShot(goodSE);
    }
    public void PlayHeartBeatSE()
    {
        audioSource.PlayOneShot(heartBeatSE);
    }
    public void PlayNextTextSE()
    {
        audioSource.PlayOneShot(nextTextSE);
    }
    public void PlayScreamSE()
    {
        audioSource.PlayOneShot(screamSE);
    }
    public void PlayBadSE()
    {
        audioSource.PlayOneShot(badSE);
    }
    public void PlayGameOverSE()
    {
        audioSource.PlayOneShot(gameoverSE);
    }
    public void PlayGameClearBGM()
    {
        audioSource.PlayOneShot(gameClearBGM);
    }
}
