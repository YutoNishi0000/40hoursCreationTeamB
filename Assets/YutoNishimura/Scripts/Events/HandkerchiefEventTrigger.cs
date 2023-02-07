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
        todayTask.TaskCompletion(2);
        GameManager.Instance.NextDay("Day 4_k");
        UIController._talkStart = false;
    }
}
