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
        //���ݓ�������
        dt = DateTime.Now;
        // �N
        text.text = dt.Year.ToString() + " / ";
        // ��
        text.text = text.text + dt.Month.ToString() + "\n";
        // ��
        text.text = text.text + dt.Hour.ToString() + " : ";
        // ��
        text.text = text.text + dt.Minute.ToString();
    }
}
