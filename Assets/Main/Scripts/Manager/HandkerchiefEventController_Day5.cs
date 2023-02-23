using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventController_Day5 : MonoBehaviour
{
    private FrontSidePlayerChecker _frontChecker;
    private TodayTask todayTask;

    Message message = null;
    GameObject messageUI = null;

    // Start is called before the first frame update
    void Start()
    {
        //_frontChecker = GetComponentInParent<FrontSidePlayerChecker>();
        _frontChecker = GameObject.Find("Collider").GetComponent<FrontSidePlayerChecker>();
        messageUI = GameObject.Find("MessageUI");
        message = messageUI.GetComponent<Message>();
        todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
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
        todayTask.TaskCompletion(3);
        Day5.day5 = true;
        //GameManager.Instance.SetInContactArea(true);
        //Debug.Log(GameManager.Instance.GetInContactArea());
    }
}
