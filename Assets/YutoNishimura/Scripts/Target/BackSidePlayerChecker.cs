using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSidePlayerChecker : MonoBehaviour
{
    private bool _isRecognizeBack;                                    //�v���C���[��F�����Ă��邩�ǂ���(����͈�)
    private FrontSidePlayerChecker _frontChecker;

    private void Start()
    {
        _frontChecker = GetComponent<FrontSidePlayerChecker>();
    }

    private void Update()
    {
        if(_frontChecker.CheckPlayerFront())
        {
            GameManager.Instance.SetInContactArea(true);
            _frontChecker.CountTimer();
        }
        else
        {
            GameManager.Instance.SetInContactArea(false);
            _frontChecker.OffTimer();
        }
    }
}
