using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSidePlayerChecker : MonoBehaviour
{
    private bool _isRecognizeBack;                                    //プレイヤーを認識しているかどうか(後方範囲)
    private FrontSidePlayerChecker _frontChecker;

    private void Start()
    {
        _frontChecker = GetComponent<FrontSidePlayerChecker>();
    }

    private void Update()
    {
        if(_frontChecker.CheckPlayerFront() || _isRecognizeBack)
        {
            _frontChecker.CountTimer();
        }
        else
        {
            _frontChecker.OffTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isRecognizeBack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isRecognizeBack = false;
        }
    }
}
