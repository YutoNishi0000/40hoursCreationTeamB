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
            Day3.day3 = true;
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

        //イベント時のテキストを表示
        _message.EventText((int)Scenario.MessageState.DAY3_GET);

        //イベントがどれぐらい達成されているかチェック
        for(int i = 0; i < GameManager.Instance.tasks.Count; i++)
        {
            //もしもタスクがDay3のタスクであれば
            if (GameManager.Instance.tasks[i].date == 2)
            {
                //そのタスクがクリアされた状態にする
                GameManager.Instance.tasks[i].isCompletion = true;
            }
        }

        int j = 0;

        //ここでもし全てのタスクがクリアされていたら
        if (GameManager.Instance.tasks[j].isCompletion && GameManager.Instance.tasks[j + 1].isCompletion)
        {
            //次のDayに移行
            _hankatiEventStart = true;
        }
        //そうでなければ
        else
        {

        }

        //_todayTask.TaskCompletion(2);
        UIController._talkStart = false;
    }
}
