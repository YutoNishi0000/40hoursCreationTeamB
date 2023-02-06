using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //インゲームイベントの種類
    public enum EVENTTYPE
    {
        noEvent,        //イベントなし
        fled,           //逃げられ演出
        handkerchief,   //ハンカチイベント
        negotiation,    //交渉イベント
    }
    public static EVENTTYPE eventType;

    //今日のタスクの処理してるオブジェクトとスクリプト
    GameObject task;
    TodayTask todayTask;
    private void Start()
    {
        task = GameObject.Find("TodayTask");
        todayTask = task.GetComponent<TodayTask>();
    }
    private void Update()
    {
        //インゲームイベントごとの処理
        switch(eventType)
        {
            case EVENTTYPE.noEvent:
                    break;
            case EVENTTYPE.fled:
                break;
            case EVENTTYPE.handkerchief:
                break;
            case EVENTTYPE.negotiation:
                break;
        }
    }
    /// <summary>
    /// ハンカチイベント
    /// </summary>
    void HandkerchiefEvent()
    {

    }
}
