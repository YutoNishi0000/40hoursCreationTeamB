using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBeat : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void FastHeartBeat()
    {
        animator.SetBool("IsFast", true);
    }
    public void IdleHeartBeat()
    {
        animator.SetBool("IsFast", false);
    }
}
