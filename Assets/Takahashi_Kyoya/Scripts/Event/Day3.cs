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
            GameManager.Instance.NextDay("Day 4_k");
            Destroy(gameObject);
        }
    }
}
