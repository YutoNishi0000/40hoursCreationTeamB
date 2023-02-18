using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day4 : MonoBehaviour
{
    public static bool day4 = false;
    //タイマー
    float timer = 0;
    //フェイドアウトするまでの時間
    float faidOutTime = 2;

    private Message message;
    private bool once;
    private TodayTask todayTask;

    private void Start()
    {
        message = GameObject.Find("MessageUI").GetComponent<Message>();
        todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
    }

    // Update is called once per frame
    void Update()
    {
        if (day4)
        {
            timer += Time.deltaTime;
            if (timer > faidOutTime)
            {
                GameManager.Instance.NextDay("Day 5_k");
                Destroy(gameObject);
            }
        }
        else if (TimeController._isTimePassed)
        {
            //タスクが一つでも残っていたら
            if (todayTask.todayTask.Count > 0)
            {
                //時間切れになったらゲームオーバー
                if (!once)
                {
                    message.EventText((int)Scenario.MessageState.DAY4_TIMEOVER);
                    once = true;
                }

                //このフラグは絶対にオフになるのでバグは心配しくてよい
                if (Message.PlayerMoveFlag)
                {
                    GameManager.Instance.NextDay("FailedNegotiation");
                    Destroy(gameObject);
                }
            }
            //タスクが一つも残っていなかったら
            else
            {
                GameManager.Instance.NextDay("Negotiation");
                Destroy(gameObject);
            }

            //GameManager.Instance.NextDay("Day 5_k");
            //Destroy(gameObject);
        }
        else if (FrontSidePlayerChecker._Escaped)
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }
}
