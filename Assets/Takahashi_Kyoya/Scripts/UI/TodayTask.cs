using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TASK
{
    //表示するタスクテキスト
    public string taskName;
    //タスクが完了しているか
    public bool isCompletion;
    //次の日に持ち越せるか
    public bool takeOver;
    //何日目のタスクか
    public int date;

    public string GetTaskName()
    {
        return taskName;
    }
    public bool GetIsCompletion()
    {
        return isCompletion;
    }
    public void CompletionTask()
    {
        isCompletion = true;
    }
}
public class TodayTask : MonoBehaviour
{
    //タスククラス
    List<TASK> tasks = new List<TASK>()
    {
        //day1
        new TASK { taskName = "”あの人”を見つけよう\n(でも見つかるな！)",
            isCompletion = false, takeOver = true , date = 0 },
        //day2
        new TASK { taskName = "ばれなように写真を\n撮ろう",                 
            isCompletion = false, takeOver = true , date = 1 },
        //day3
        new TASK { taskName = "？？？\n(とりあえず”あの人”を探そう)", 
            isCompletion = false, takeOver = false, date = 2 },
        //day4
        new TASK { taskName = "ハンカチを返そう",                       
            isCompletion = false, takeOver = false, date = 3 },
        //day5
        new TASK { taskName = "”あの人”に話しかけよう",               
            isCompletion = false, takeOver = false, date = 4 },
    };
    //タスクが完了しているかのUI
    [SerializeField] List<Toggle> toggle = new List<Toggle>();
    //タスクを表示するテキストUI
    [SerializeField] List<Text> taskText = new List<Text>();

    //今日のタスク
    List<string> todayTask = new List<string>();

    private void Start()
    {
        Initialize();
        CheckTodayTask();
        DisplayTask();
    }
    /// <summary>
    /// 今日のタスクの確認
    /// </summary>
    void CheckTodayTask()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            //前の日があるかどうかの確認
            if (GameManager.Instance.GetDate() - 1 > 0)
            {
                //前日の終わってないタスクの確認
                if (GameManager.Instance.GetDate() - 1 == tasks[i].date)
                {
                    //持ち越せるタスクかの確認
                    if (tasks[i].takeOver == true)
                    {
                        //終わってるかの確認
                        if (tasks[i].isCompletion == false)
                            todayTask.Add(tasks[i].taskName);
                    }
                }
            }
            //今日のタスクの確認
            if (GameManager.Instance.GetDate() == tasks[i].date)
                todayTask.Add(tasks[i].taskName);
        }
    }
    /// <summary>
    /// 今日のタスクを表示
    /// </summary>
    private void DisplayTask()
    {
        for (int i = 0; i < todayTask.Count; i++)
        {
            taskText[i].gameObject.SetActive(true);
            toggle[i].gameObject.SetActive(true);
            taskText[i].text = todayTask[i].ToString();
            toggle[i].isOn = tasks[i].GetIsCompletion();
        }
    }
    /// <summary>
    /// 初期化
    /// </summary>
    private void Initialize()
    {
        todayTask.Clear();
        for(int i = 0; i < taskText.Count; i++)
        {
            taskText[i].gameObject.SetActive(false);
            toggle[i].gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// タスクが完了したときに呼ばれる
    /// </summary>
    public void TaskCompletion(int taskIndex)
    {
        Debug.Log("タスク完了");
        tasks[taskIndex].CompletionTask();
        for (int i = 0; i < todayTask.Count; i++)
        {
            toggle[i].isOn = tasks[i].GetIsCompletion();
        }
    }
    /// <summary>
    /// タスクがすべて終わっているかの確認
    /// </summary>
    /// <returns>タスクがすべて終わってたらtrue すべて終わってなかったらfalse</returns>
    public bool IsAllTaskComletion()
    {
        int Comletion = 0;
        //タスクがすべて終わっているか
        for (int i = 0; i < toggle.Count; i++)
        {
            if (toggle[0].isOn && toggle[1].isOn)
            {
                Comletion++;
            }
        }
        if(tasks.Count == Comletion)
        {
            return true;
        }

        return false;
    }
}