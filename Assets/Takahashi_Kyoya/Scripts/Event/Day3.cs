using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day3 : MonoBehaviour
{
    public static bool day3 = false;
    //タイマー
    float timer = 0;
    //フェイドアウトするまでの時間
    float faidOutTime = 5;

    private TodayTask todayTask;
    private Message message;
    private bool once;

    private void Start()
    {
        once = false;
        todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
        message = GameObject.Find("MessageUI").GetComponent<Message>();
    }

    void Update()
    {
        if (day3)
        {
            timer += Time.deltaTime;
            if (timer > faidOutTime)
            {
                GameManager.Instance.NextDay("Day 4_k");
                Destroy(gameObject);
            }
        }
        else if (TimeController._isTimePassed)
        {
            //タスクが一つでも残っていたら
            if(todayTask.todayTask.Count > 0)
            {
                if (!once)
                {
                    message.EventText((int)Scenario.MessageState.DAY3_TIMEOVER);
                    once = true;
                }

                //このフラグは絶対にオフになるのでバグは心配しくてよい
                if (Message.PlayerMoveFlag)
                {
                    GameManager.Instance.GameOver();
                    Destroy(gameObject);
                }
            }
            else
            {
                GameManager.Instance.NextDay("Day 4_k");
                Destroy(gameObject);
            }
        }
    }
}
