using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�Q�[�����̎��Ԃ𐧌䂷��֐�
public class TimeController : MonoBehaviour
{
    public static bool _isTimePassed;
    public Text timeText;
    private float TimeLeft = 120;      //�c�莞��
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

    //�c�莞�Ԃ�\��
    void OnTime(float leftTime, Text time)
    {
        if(leftTime <= 0)
        {
            _isTimePassed = true;
            time.text = "TIME OVER...";
            return;
        }


        time.text = "�c��" + Mathf.FloorToInt(leftTime / 60) + "��" + Mathf.FloorToInt(leftTime % 60) + "�b";
    }

    public bool GetTimePassedFlag()
    {
        return _isTimePassed;
    }
}
