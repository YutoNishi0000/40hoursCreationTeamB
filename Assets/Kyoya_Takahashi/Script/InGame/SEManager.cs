using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : SingletonMonoBehaviour<SEManager>
{
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private AudioClip respawn;
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip skill;
    [SerializeField] private AudioClip targetShot;
    [SerializeField] private GameObject timeLimit;
    [SerializeField] private AudioClip plusCountSE;
    [SerializeField] private AudioClip minusCountSE;
    //===== アウトゲーム =====
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip decision;
    [SerializeField] private AudioClip back;

    [SerializeField] private readonly int maxAudioSources = 10;     //生成するオーディオソースの最大数

    private bool isTimeLimit = false;

    private void Start()
    {
        audioSources = new AudioSource[maxAudioSources];
        for (int i = 0; i < audioSources.Length; ++i)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// 利用可能なオーディオソースを取得する
    /// </summary>
    /// <returns></returns>
    private AudioSource GetAvailableAudioSource()
    {
        for(int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] == null)
            {
                Debug.Log("合うぢおそーすがnullです");
            }

            if (!audioSources[i].isPlaying)
            {
                return audioSources[i];
            }
        }

        return null;
    }

    private void oneShot(AudioClip ac)
    {
        AudioSource ad = GetAvailableAudioSource();
        ad.PlayOneShot(ac);
    }
    /// <summary>
    /// ターゲットリスポーン時のSE
    /// </summary>
    public void PlayRespawn()
    {
        oneShot(respawn);
    }
    /// <summary>
    /// 撮影時のSE
    /// </summary>
    public void PlayShot()
    {
        oneShot(shot);
    }
    /// <summary>
    /// スキル発動時のSE
    /// </summary>
    public void PlaySkill()
    {
        oneShot(skill);
    }
    /// <summary>
    /// ターゲット撮影成功時のSE
    /// </summary>
    public void PlayTargetShot()
    {
        oneShot(targetShot);
    }
    /// <summary>
    /// タイムカウントがプラスされたときのSE
    /// </summary>
    public void PlayPlusTimeCountSE()
    {
        oneShot(plusCountSE);
    }
    /// <summary>
    /// タイムカウントがマイナスされた時のSE
    /// </summary>
    public void PlayMinusTimeCountSE()
    {
        oneShot(minusCountSE);
    }
    /// <summary>
    /// タイムリミットに到達時のSE
    /// </summary>
    /// <param name="playTime">再生時間</param>
    public void PlayTimeLimit(float playTime)
    {        
        if (!isTimeLimit)
        {
            Instantiate(timeLimit);
        }
        isTimeLimit = true;
    }
    //===== アウトゲーム =====
    public void PlaySelect()
    {
        oneShot(select);
    }
    public void PlayDecision()
    {
        oneShot(decision);
    }
    public void PlayBack()
    {
        oneShot(back);
    }

}
