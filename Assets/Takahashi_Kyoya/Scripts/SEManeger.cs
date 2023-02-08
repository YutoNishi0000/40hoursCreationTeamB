using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManeger : MonoBehaviour
{
    public static AudioSource audioSource = null;
    //SE
    [SerializeField] public static AudioClip cameraSE = null;
    [SerializeField] public static AudioClip goodSE = null;
    [SerializeField] public static AudioClip heartBeatSE = null;
    [SerializeField] public static AudioClip nextTextSE = null;
    [SerializeField] public static AudioClip screamSE = null;
    [SerializeField] public static AudioClip badSE = null;
    [SerializeField] public static AudioClip gameoverSE = null;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayCameraSE()
    {
        audioSource.PlayOneShot(cameraSE);
    }
    public static void PlayGoodSE()
    {
        audioSource.PlayOneShot(goodSE);
    }
    public static void PlayHeartBeatSE()
    {
        audioSource.PlayOneShot(heartBeatSE);
    }
    public static void PlayNextTextSE()
    {
        audioSource.PlayOneShot(nextTextSE);
    }
    public static void PlayScreamSE()
    {
        audioSource.PlayOneShot(screamSE);
    }
    public static void PlayBadSE()
    {
        audioSource.PlayOneShot(badSE);
    }
    public static void PlayGameOverSE()
    {
        audioSource.PlayOneShot(gameoverSE);
    }

}
