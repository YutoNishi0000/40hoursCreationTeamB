using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventTrigger : MonoBehaviour
{
    private TodayTask todayTask;

    private void Start()
    {
        todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
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
        todayTask.TaskCompletion(2);
        GameManager.Instance.NextDay("Day 4_k");
        UIController._talkStart = false;
    }
}
