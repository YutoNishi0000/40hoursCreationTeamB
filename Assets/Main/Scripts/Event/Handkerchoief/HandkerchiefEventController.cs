using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventController : MonoBehaviour
{
    private FrontSidePlayerChecker _frontChecker;
    private TodayTask todayTask;
    private bool _hankatiEventStart;
    private TargetController target;

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
        target = GetComponentInParent<TargetController>();
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
            Day4.day4 = true;
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
        //todayTask.TaskCompletion(3);
        message.EventText((int)Scenario.MessageState.DAY4_GET);
        _hankatiEventStart = true;
        target.SettargetState(TargetController.TargetState.LookPlayer);
        _frontChecker.TriggerEvent();
        //GameManager.Instance.SetInContactArea(true);
        //Debug.Log(GameManager.Instance.GetInContactArea());
    }
}
