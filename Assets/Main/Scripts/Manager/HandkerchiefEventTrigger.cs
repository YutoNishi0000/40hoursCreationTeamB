using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventTrigger : MonoBehaviour
{
    private TodayTask _todayTask;
    private Message _message;
    private bool _hankatiEventStart;
    private ChangeCameraAngle changeCamera;

    private void Start()
    {
        _hankatiEventStart = false;
        changeCamera = GetComponent<ChangeCameraAngle>();
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
        for(int i = 0; i < _todayTask.todayTask.Count; i++)
        {
            //もしもタスクがDay3のタスクであれば
            if (GameManager.Instance.tasks[i].date == 2)
            {
                //そのタスクがクリアされた状態にする
                GameManager.Instance.tasks[i].isCompletion = true;
            }
        }

        int j = 0;

        if (_todayTask.todayTask.Count == 2 && GameManager.Instance.tasks[j].isCompletion && GameManager.Instance.tasks[j + 1].isCompletion)
        {
            //ここでもし全てのタスクがクリアされていたら
            //次のDayに移行
            _hankatiEventStart = true;
        }
        //タスクの数が一個で
        else if (_todayTask.todayTask.Count == 1 && GameManager.Instance.tasks[j].isCompletion)
        {
            //ここでもし全てのタスクがクリアされていたら
            //次のDayに移行
            _hankatiEventStart = true;
        }
        //どの条件にも当てはまらず、テキスト表示が終了していたら
        else if(!Message.PlayerMoveFlag)
        {
            //プレイヤーの元の視点に戻す
            changeCamera.ExitVoyeurism();
        }

        //_todayTask.TaskCompletion(2);
        UIController._talkStart = false;
    }
}
