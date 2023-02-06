using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MessageData
{
    public string massage { get; set; } 
    public Color color { get; set; }
}
public class Message : MonoBehaviour
{
    public Text messageText;
    private GameObject Panel;
    public int count = 0;       // �f�o�b�O�̂���public
    public int DayNumber = 0;   // �f�o�b�O�̂���public
    List<MessageData> TodayMes = null;
    [SerializeField] private Text text;
    [SerializeField] string DebugDayNumber = "�����ڂ̃��b�Z�[�W��\�����܂����H";

    static List<List<MessageData>> list = new List<List<MessageData>>()
    {
        Scenario.meslist_day1,
        Scenario.meslist_day2,
        Scenario.meslist_day3,
        Scenario.meslist_day4,
        Scenario.meslist_day5,
    };

    private void Start()
    {
        // �e�평����
        Init();

        Panel = transform.GetChild(0).gameObject;
        DayNumber = GetDayNamber();
#if !UNITY_EDITOR
        TodayMes = GetMesList(int.Parse(DebugDayNumber));
#else
        TodayMes = GetMesList(DayNumber);
#endif
    }
    private void Update()
    {

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("������ĂȂ�");
            return;
        }

        if (TodayMes.Count > count)
        {
            if (GetMes(count).massage == "#")
            {
                count++;
                Panel.SetActive(false);
                return;
            }
            SetMes(count);
            count++;
        }
        else
        {
            Panel.SetActive(false);
        }
    }

    private void Init()
    {
        count = 0;
        DayNumber = 0;
    }

    public void SetMes(int num)
    {
        text.text = GetMes(num).massage;
        text.color = GetMes(num).color;
    }
    private MessageData GetMes(int num)
    {
        return TodayMes[num];
    }
    private List<MessageData> GetMesList(int DayNumber)
    {
#if !UNITY_EDITOR
        return list[DayNumber - 1];
#else
        return list[DayNumber];
#endif
    }
    private int GetDayNamber()
    {
#if !UNITY_EDITOR
        string ActiveScene = SceneManager.GetActiveScene().name;
        return Convert.ToInt32(ActiveScene[3]);
#else
        // GameManager���猻�݂�Day�ԍ��i0-4�j���擾
        return DayNumber;
#endif
    }
    // �f�o�b�O�p
    //public void DebugMessageDisplayDay1()
    //{
    //    Init();
    //    DayNumber = 0;
    //    TodayMes = GetMesList(DayNumber);
    //}
    // �f�o�b�O�p
    //public void DebugMessageDisplayDay2()
    //{
    //    Init();
    //    DayNumber = 1;
    //    TodayMes = GetMesList(DayNumber);
    //}
}