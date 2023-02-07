using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventTrigger : MonoBehaviour
{
    private TodayTask _todayTask;
    private Message _message;
    private bool _hankatiEventStart;

    private void Start()
    {
        _hankatiEventStart = false;
        _todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
        _message = GameObject.Find("MessageUI").GetComponent<Message>();
    }

    private void Update()
    {
        if(!Message.TextEventFlag && _hankatiEventStart)
        {
            GameManager.Instance.NextDay("Day 4_k");
            _hankatiEventStart = false;
        }
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
        _message.EventText((int)Scenario.MessageState.DAY3_GET);
        _todayTask.TaskCompletion(2);
        UIController._talkStart = false;
        _hankatiEventStart = true;
    }
}
