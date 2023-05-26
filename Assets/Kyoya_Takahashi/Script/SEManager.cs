using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : SingletonMonoBehaviour<SEManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip respawn;
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip skill;
    [SerializeField] private AudioClip targetShot;
    [SerializeField] private GameObject timeLimit;

    private bool isTimeLimit = false;
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
}
