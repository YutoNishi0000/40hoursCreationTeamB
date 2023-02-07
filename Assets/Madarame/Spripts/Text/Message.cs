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
    //private GameObject Player;   // プレイヤーのアクティブをtrue/falseさせる
    public int count = 0;       // デバッグのためpublic
    public int DayNumber = 0;   // デバッグのためpublic
    List<MessageData> TodayMes = null;

    [SerializeField] private Text text;
    [SerializeField] string DebugDayNumber = "何日目のメッセージを表示しますか？";

    // テキストイベント中か
    public static bool TextEventFlag = false;

    // テキストイベント時false
    public static bool PlayerMoveFlag = false;

    static List<List<MessageData>> list = new List<List<MessageData>>()
    {
        Scenario.meslist_day1,
        Scenario.meslist_day2,
        Scenario.meslist_day3,
        Scenario.meslist_day4,
        Scenario.meslist_day5,
        Scenario.meslist_GameClear, //5
        Scenario.meslist_GameOver,  //6
        Scenario.meslist_day3_gethankati,
        Scenario.meslist_day3_timeover,
        Scenario.meslist_day4_gethankati,
        Scenario.meslist_day4_timeover
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

        //最初にメッセージを表示
        StartMes();
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
                TextEventFlag = false;
                Panel.SetActive(TextEventFlag);
                
                //プレイヤーの移動が可能
                PlayerMoveFlag = true;
                return;
            }
            SetMes(count);
            count++;
        }
        else
        {
            TextEventFlag = false;
            Panel.SetActive(TextEventFlag);
        }
        Debug.Log("");
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
    public void EventText(int num)
    {
        count = 0;
        SetEvent(num);
        SetMes(count);
        count++;
        TextEventFlag = true;
        Panel.SetActive(TextEventFlag);
        //プレイヤーの移動が可能
        PlayerMoveFlag = false;
    }

    public void SetEvent(int num)
    {
        TodayMes = null;
        TodayMes = list[num + 7];
    }

    
    public void StartMes()
    {
        if (TodayMes.Count > count)
        {
            SetMes(count);
            count++;
        }
    }
    // ゲームクリアテキストを呼ぶ
    public List<MessageData> GetGameClearText()
    {
        int GameClear = 5;
        return list[GameClear];
    }
    // ゲームクリアテキストを表示
    public void DrawGC_Text()
    { 
        TodayMes = GetGameClearText();
        Panel.SetActive(true);
        //プレイヤーの移動が不可能
        PlayerMoveFlag = false;
    }
    // ゲームオーバーテキストを呼ぶ
    public List<MessageData> GameOverText()
    {
        int GameOver = 6;
        return list[GameOver];
    }
    // ゲームオーバーテキストを表示
    public void DrawGO_Text()
    {
        TodayMes = GameOverText();
        Panel.SetActive(true);
        //プレイヤーの移動が不可能
        PlayerMoveFlag = false;
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