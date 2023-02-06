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
    private GameObject Panel;   // UIを表示/非表示させる
    private GameObject Player;   // プレイヤーのアクティブをtrue/falseさせる
    public int count = 0;       // デバッグのためpublic
    public int DayNumber = 0;   // デバッグのためpublic
    List<MessageData> TodayMes = null;
    [SerializeField] private Text text;
    [SerializeField] string DebugDayNumber = "何日目のメッセージを表示しますか？";

    // テキストイベント時false
    public static bool PlayerMoveFlag = false;

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
        //ゲームスタート時、プレイヤーの移動が不可
        PlayerMoveFlag = false;

        // 各種初期化
        Init();

        Panel = transform.GetChild(0).gameObject;
        DayNumber = GetDayNamber();

        TodayMes = GetMesList(DayNumber);
    }

    private void Update()
    {

        if (!Input.GetKeyDown(KeyCode.E))
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
                //プレイヤーの移動が可能
                PlayerMoveFlag = true;
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
        return list[DayNumber];
    }
    private int GetDayNamber()
    {
        // GameManagerから現在のDay番号（0-4）を取得
        return GameManager.Instance.GetDate();
    }
   
    // デバッグ用
    //public void DebugMessageDisplayDay1()
    //{
    //    Init();
    //    DayNumber = 0;
    //    TodayMes = GetMesList(DayNumber);
    //}
    // デバッグ用
    //public void DebugMessageDisplayDay2()
    //{
    //    Init();
    //    DayNumber = 1;
    //    TodayMes = GetMesList(DayNumber);
    //}
}