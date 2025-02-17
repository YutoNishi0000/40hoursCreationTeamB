using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowTime : MonoBehaviour
{
    DateTime dt;
    public Text text;

    void Start()
    {
        //現在日時を代入
        dt = DateTime.Now;
        // 年
        text.text = dt.Year.ToString() + " / ";
        // 月
        text.text = text.text + dt.Month.ToString() + " . ";
        // 時
        text.text = text.text + dt.Hour.ToString() + " : ";
        // 分
        text.text = text.text + dt.Minute.ToString();
    }
}
