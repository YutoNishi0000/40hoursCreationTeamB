using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventTrigger : MonoBehaviour
{
    private TodayTask _todayTask;
    private Message _message;

    private void Start()
    {
        _todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
        _message = GameObject.Find("MessageUI").GetComponent<Message>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Handkerchief"))
        {
            HandkerchiefEvent();
        }
    }

    /// <summary>
    /// �n���J�`���E�����Ƃ��ɏ�������֐�
    /// </summary>
    void HandkerchiefEvent()
    {
        //======================================================================
        //======================================================================
        //
        // �����Ƀn���J�`���E�����Ƃ��̏������L�q���Ă�������
        //
        //======================================================================
        //======================================================================

        Debug.Log("�n���J�`���E�������̃C�x���g����");
        _message.EventText();

        if(!Message.PlayerMoveFlag)
        {
            return;
        }

        _todayTask.TaskCompletion(2);
        GameManager.Instance.NextDay("Day 4_k");
        UIController._talkStart = false;
    }
}
