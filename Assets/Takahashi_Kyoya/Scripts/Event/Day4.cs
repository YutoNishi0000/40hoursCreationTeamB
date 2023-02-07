using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day4 : MonoBehaviour
{
    public static bool day4 = false;
    //タイマー
    float timer = 0;
    //フェイドアウトするまでの時間
    float faidOutTime = 5;

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
            GameManager.Instance.NextDay("Day 5_k");
            Destroy(gameObject);
        }
    }
}
