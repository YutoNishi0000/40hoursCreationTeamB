using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ゲーム内の時間を制御する関数
public class TimeController : MonoBehaviour
{
    public static bool _isTimePassed;
    public Text timeText;
    private float TimeLeft = 120;      //残り時間
    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        _isTimePassed = false;
        _time = TimeLeft;
        OnTime(TimeLeft, timeText);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Message.PlayerMoveFlag)
        {
            return;
        }

        _time -= Time.deltaTime;
        OnTime(_time, timeText);
    }

    //残り時間を表示
    void OnTime(float leftTime, Text time)
    {
        if(leftTime <= 0)
        {
            _isTimePassed = true;
            time.text = "TIME OVER...";
            return;
        }


        time.text = "残り" + Mathf.FloorToInt(leftTime / 60) + "分" + Mathf.FloorToInt(leftTime % 60) + "秒";
    }

    public bool GetTimePassedFlag()
    {
        return _isTimePassed;
    }
}
