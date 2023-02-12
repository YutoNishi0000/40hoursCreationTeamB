using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBeat : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    //”“®SE‚Ç‚Á‚­‚ñ‚Ì‚Ç‚Á
    [SerializeField] private AudioClip heartBeatF;
    //”“®SE‚Ç‚Á‚­‚ñ‚Ì‚­‚ñ
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
    /// ”“®SE‚Ì‚Ç‚Á‚ğ–Â‚ç‚·
    /// </summary>
    public void HeartBeatSEF()
    {
        audioSource.PlayOneShot(heartBeatF);
    }
    /// <summary>
    /// ”“®SE‚Ì‚­‚ñ‚ğ–Â‚ç‚·
    /// </summary>
    public void HeartBeatSES()
    {
        audioSource.PlayOneShot(heartBeatS);
    }
}
