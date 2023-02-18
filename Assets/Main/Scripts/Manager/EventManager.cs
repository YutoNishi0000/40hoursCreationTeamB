using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //プレイヤーオブジェクト
    private GameObject player = null;
    //ターゲットオブジェクト
    private GameObject target = null;

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
    private void Awake()
    {
        Message.PlayerMoveFlag = true;
    }
    private void Start()
    {
        task = GameObject.Find("TodayTask");
        todayTask = task.GetComponent<TodayTask>();
        player = GameObject.FindWithTag("Player");
        target = GameObject.FindWithTag("Target");
        eventType = EVENTTYPE.noEvent;
    }
    private void Update()
    {
        ////インゲームイベントごとの処理
        //switch(eventType)
        //{
        //    case EVENTTYPE.noEvent:
        //            break;
        //    case EVENTTYPE.fled:

        //        break;
        //    case EVENTTYPE.handkerchief:
        //        if (GameManager.Instance.GetInContactArea())
        //        {
        //            Debug.Log("InContactAreaの中");
        //            HandkerchiefEvent();
        //        }
        //        break;
        //    case EVENTTYPE.negotiation:
        //        break;
        //}
        //switch (GameManager.Instance.GetDate())
        //{
        //    case 0:
        //        break;
        //    case 1:
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //    case 4:
        //        break;
        //}

    }
    /// <summary>
    /// 逃げられイベント
    /// </summary>
    void FledEvent()
    {
        //GameManager.Instance.NextDay();
    }
    /// <summary>
    /// ハンカチイベント
    /// </summary>
    void HandkerchiefEvent()
    {
        if (GameManager.Instance.GetInContactArea())
        {
            Debug.Log("こっち見てる");
            //player.transform.LookAt(target.transform.position);
            //target.transform.LookAt(player.transform.position);
            //Message.PlayerMoveFlag = false;
        }
    }
    /// <summary>
    /// 交渉イベント
    /// </summary>
    void NegotiationEvent()
    {
        GameManager.Instance.GameClear();
    }
}
