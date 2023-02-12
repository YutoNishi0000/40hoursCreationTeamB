using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBeat : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    //����SE�ǂ�����̂ǂ�
    [SerializeField] private AudioClip heartBeatF;
    //����SE�ǂ�����̂���
    [SerializeField] private AudioClip heartBeatS;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void FastHeartBeat()
    {
        animator.SetBool("IsFast", true);
    }
    public void IdleHeartBeat()
    {
        animator.SetBool("IsFast", false);
    }
    /// <summary>
    /// ����SE�̂ǂ���炷
    /// </summary>
    public void HeartBeatSEF()
    {
        audioSource.PlayOneShot(heartBeatF);
    }
    /// <summary>
    /// ����SE�̂����炷
    /// </summary>
    public void HeartBeatSES()
    {
        audioSource.PlayOneShot(heartBeatS);
    }
}
