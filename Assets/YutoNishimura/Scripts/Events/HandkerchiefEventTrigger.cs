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
    /// ハンカチを拾ったときに処理する関数
    /// </summary>
    void HandkerchiefEvent()
    {
        //======================================================================
        //======================================================================
        //
        // ここにハンカチを拾ったときの処理を記述してください
        //
        //======================================================================
        //======================================================================

        Debug.Log("ハンカチを拾った時のイベント発生");
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
