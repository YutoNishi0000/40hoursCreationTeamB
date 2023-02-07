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
    void Update()
    {
        if (day2)
        {
            timer += Time.deltaTime;
            if (timer > faidOutTime)
            {
                GameManager.Instance.NextDay("Day 3_k");
                Destroy(gameObject);
            }
        }
    }
}
