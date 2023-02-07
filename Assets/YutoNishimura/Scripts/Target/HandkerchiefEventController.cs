using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventController : MonoBehaviour
{
    private FrontSidePlayerChecker _frontChecker;

    Message message = null;
    GameObject messageUI = null;

    // Start is called before the first frame update
    void Start()
    {
        _frontChecker = GetComponent<FrontSidePlayerChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_frontChecker.CheckPlayerFront())
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
        if(other.gameObject.CompareTag("Player"))
        {
            HankatiEvent();
        }
    }

    void HankatiEvent()
    {
        //=================================================================================================
        //=================================================================================================
        //
        // �����Ƀn���J�`�C�x���g�̉�b�V�[���Ȃǂ̏������L�q���Ă�������
        //
        //=================================================================================================
        //=================================================================================================
        Debug.Log("�n���J�`�C�x���g����");
        
        //GameManager.Instance.SetInContactArea(true);
        //Debug.Log(GameManager.Instance.GetInContactArea());
    }
}
