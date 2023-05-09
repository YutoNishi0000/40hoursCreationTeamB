using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2 : MonoBehaviour
{
    public static bool day2 = false;
    //タイマー
    float timer = 0;
    //フェイドアウトするまでの時間
    float faidOutTime = 3;

    private TodayTask todayTask;

    private void Start()
    {
        todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
    }

    //void Update()
    //{
    //    if (day2)
    //    {
    //        timer += Time.deltaTime;
    //        if (timer > faidOutTime)
    //        {
    //            GameManager.Instance.NextDay("Day 3_k");
    //            Destroy(gameObject);
    //        }
    //    }
    //    else if(TimeController._isTimePassed)
    //    {
    //        //もしもDay1のタスクが終わっていなければ
    //        for(int i = 0; i < todayTask.todayTask.Count; i++) 
    //        {
    //            if (todayTask.todayTask[i] == GameManager.Instance.GetTaskName(GameManager.Instance.GetDate() - 1))
    //            {
    //                GameManager.Instance.GameOver();
    //                Destroy(gameObject);
    //                return;
    //            }
    //        }

    //        GameManager.Instance.NextDay("Day 3_k");
    //        Destroy(gameObject);
    //    }
    //}
}
