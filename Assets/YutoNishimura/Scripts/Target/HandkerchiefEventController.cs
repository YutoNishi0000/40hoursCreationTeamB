using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventController : MonoBehaviour
{
    private FrontSidePlayerChecker _frontChecker;
    private TodayTask todayTask;
    private bool _hankatiEventStart;

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
        _hankatiEventStart = false;
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

        if(!Message.TextEventFlag && _hankatiEventStart)
        {
            GameManager.Instance.NextDay("Day 5_k");
            _hankatiEventStart = false;
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
        // ここにハンカチイベントの会話シーンなどの処理を記述してください
        //
        //=================================================================================================
        //=================================================================================================
        Debug.Log("ハンカチイベント発生DAY4");
        todayTask.TaskCompletion(3);
        message.EventText();
        _hankatiEventStart = true;
        //GameManager.Instance.SetInContactArea(true);
        //Debug.Log(GameManager.Instance.GetInContactArea());
    }
}
