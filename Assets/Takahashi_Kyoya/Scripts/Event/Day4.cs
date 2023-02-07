using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Day4 : MonoBehaviour
{
    public static bool day4 = false;
    //タイマー
    float timer = 0;
    //フェイドアウトするまでの時間
    float faidOutTime = 5;

    private Message message;

    private void Start()
    {
        message = GameObject.Find("MessageUI").GetComponent<Message>();
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
            //時間切れになったらゲームオーバー

            message.EventText((int)Scenario.MessageState.DAY4_TIMEOVER);

            //このフラグは絶対にオフになるのでバグは心配しくてよい
            if (Message.PlayerMoveFlag)
            {
                GameManager.Instance.GameOver();
                Destroy(gameObject);
            }

            //GameManager.Instance.NextDay("Day 5_k");
            //Destroy(gameObject);
        }
    }
}
