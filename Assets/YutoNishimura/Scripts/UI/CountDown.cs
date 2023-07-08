using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CountDown : MonoBehaviour
{
    //ピボットはスクリーンの中心にしてください

    [SerializeField] private Image Three;
    [SerializeField] private Image Two;
    [SerializeField] private Image One;
    [SerializeField] private Image StartPanel;
    private Vector3 InitialThreeScale;
    private Vector3 InitialTwoScale;
    private Vector3 InitialOneScale;
    private Vector3 InitialStartPanelScale;
    [SerializeField] private float magnification;  //何倍に拡大するか
    private float time;
    private readonly float CountTime = 5;
    private static bool finishCountDown; 

    void Start()
    {
        finishCountDown = false;
        InitialThreeScale = Three.transform.localScale;
        InitialTwoScale = Two.transform.localScale;
        InitialOneScale = One.transform.localScale;
        InitialStartPanelScale = StartPanel.transform.localScale;
        Three.transform.localScale *= magnification;
        Two.transform.localScale *= magnification;
        One.transform.localScale *= magnification;
        StartPanel.transform.localScale *= magnification;
        Three.enabled = false;
        Two.enabled = false;
        One.enabled = false;
        StartPanel.enabled = false;
        time = CountTime;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        CountDownControl();
        if (!CountDownTimer.startFinishCountDown)
        {
            return;
        }
        if (0 < time)
        {
            return ;
        }
        time = CountTime;
        Three.transform.localScale *= magnification;
        Two.transform.localScale *= magnification;
        One.transform.localScale *= magnification;
        StartPanel.transform.localScale *= magnification;
    }

    public void CountDownControl()
    {
        if (time < -1)
        {
            return;
        }

        Debug.Log(time);
        Debug.Log("通ってるけん安心して");
        time -= Time.deltaTime;

        if(time > 3 && time <= 4)
        {
            Three.enabled = true;
            Three.transform.localScale = Vector3.Lerp(Three.transform.localScale, InitialThreeScale, 0.5f);
        }
        else if (time > 2 && time <= 3)
        {
            Three.enabled = false;
            Two.enabled = true;
            Two.transform.localScale = Vector3.Lerp(Two.transform.localScale, InitialTwoScale, 0.5f);
        }
        else if(time > 1 && time <= 2)
        {
            Two.enabled = false;
            One.enabled = true;
            One.transform.localScale = Vector3.Lerp(One.transform.localScale, InitialOneScale, 0.5f);
        }
        else if(time > 0 && time <= 1)
        {
            One.enabled = false;
            StartPanel.enabled = true;
            StartPanel.transform.localScale = Vector3.Lerp(StartPanel.transform.localScale, InitialStartPanelScale, 0.5f);
        }
        else if(time <= 0)
        {
            //Color color = StartPanel.GetComponent<Image>().color;
            //color.a = Mathf.Abs(time);
            //StartPanel.GetComponent<Image>().color = color;
            StartPanel.enabled = false;
            GameManager.Instance.IsPlayGame = true;
            finishCountDown = true;
        }
    }

    public static bool GetFinishCountDown() { return finishCountDown; }
}