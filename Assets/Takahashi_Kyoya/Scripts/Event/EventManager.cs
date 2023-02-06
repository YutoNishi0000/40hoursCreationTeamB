using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //今日のタスクの処理してるオブジェクトとスクリプト
    GameObject task;
    TodayTask todayTask;
    private void Start()
    {
        task = GameObject.Find("TodayTask");
        todayTask = task.GetComponent<TodayTask>();
    }
    //ゲーム終わるときの判定
}
