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
    private int count = 0;
    int DayNumber = 0;
    List<MessageData> TodayMes = null;
    [SerializeField] private Text text;
    [SerializeField] string DebugDayNumber = "何日目のメッセージを表示しますか？";

    static List<List<MessageData>> list = new List<List<MessageData>>()
    {
        Scenario.meslist_day1,
        Scenario.meslist_day2,
        Scenario.meslist_day3,
    };

    private void Start()
    {
        Panel = transform.GetChild(0).gameObject;
        DayNumber = GetDayNamber();
#if UNITY_EDITOR
        TodayMes = GetMesList(int.Parse(DebugDayNumber));
#else
        TodayMes = GetMesList(DayNumber);
#endif
    }
    private void Update()
    {
        
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("押されてない");
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
        return list[DayNumber - 1];
    }
    private int GetDayNamber()
    {
        string ActiveScene = SceneManager.GetActiveScene().name;
        return Convert.ToInt32(ActiveScene[3]);
    }
}