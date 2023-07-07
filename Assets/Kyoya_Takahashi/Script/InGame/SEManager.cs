using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : SingletonMonoBehaviour<SEManager>
{
    private AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
    }

    private void oneShot(AudioClip ac)
    {
        audioSource.PlayOneShot(ac);
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

    public AudioSource GetSEAudioSources() { return audioSource; }

}
