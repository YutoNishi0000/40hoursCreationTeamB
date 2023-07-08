using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class SEManager : SingletonMonoBehaviour<SEManager>
{
    private AudioSource[] audioSource;
    [SerializeField] private AudioClip respawn;
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip skill;
    [SerializeField] private GameObject targetShot;
    [SerializeField] private GameObject timeLimit;
    [SerializeField] private AudioClip plusCountSE;
    [SerializeField] private AudioClip minusCountSE;
    [SerializeField] private AudioClip targetEffectSE;
    //===== アウトゲーム =====
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip decision;
    [SerializeField] private AudioClip back;

    [SerializeField] private readonly int maxAudioSources = 15;     //生成するオーディオソースの最大数
    private CancellationToken token;

    private bool isTimeLimit = false;

    private void Start()
    {
        audioSource = new AudioSource[maxAudioSources];
        for(int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    private void oneShot(AudioClip ac)
    {
        AudioSource ad = GetSEAudioSources();
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
        GameObject se = Instantiate(targetShot);
        AudioSource ad = se.GetComponent<AudioSource>();
        UniTaskUpdate(() => { ad.Play(); }, null, () => { ad.Stop(); Destroy(se); }, () => { return (ad.time >= 2); }, token, UniTaskCancellMode.Auto).Forget();
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
    /// <summary>
    /// ターゲットが撮影されたときに流すSE
    /// </summary>
    public void PlayTargetEffectSE()
    {
        oneShot(targetEffectSE);
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

    public AudioSource GetSEAudioSources()
    {
        for(int i = 0; i < audioSource.Length; i++)
        {
            if (!audioSource[i].isPlaying)
            {
                return audioSource[i];
            }
        }

        return null;
    }

}
