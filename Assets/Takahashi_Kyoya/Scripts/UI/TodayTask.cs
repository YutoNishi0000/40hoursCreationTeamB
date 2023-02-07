using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class TodayTask : MonoBehaviour
{
    
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
        //前の日があるかどうかの確認
        if (GameManager.Instance.GetDate() - 1 > 0)
        {
            //前日の終わってないタスクの確認
            if (!GameManager.Instance.GetIsCompletion(GameManager.Instance.GetDate() - 1))
            {
                //持ち越せるタスクかの確認
                if (GameManager.Instance.GetTakeOver(GameManager.Instance.GetDate() - 1))
                {
                    todayTask.Add(GameManager.Instance.GetTaskName(GameManager.Instance.GetDate() - 1));
                }
            }
        }
        for (int i = 0; i < GameManager.Instance.GetCount(); i++)
        {            
            //今日のタスクの確認
            if (GameManager.Instance.GetTaskDate(i) == GameManager.Instance.GetDate())
                todayTask.Add(GameManager.Instance.GetTaskName(i));
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
            toggle[i].isOn = GameManager.Instance.GetIsCompletion(i);
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
            toggle[i].isOn = false;
            toggle[i].gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// タスクが完了したときに呼ばれる
    /// </summary>
    public void TaskCompletion(int taskIndex)
    {
        Debug.Log("タスク完了");
        GameManager.Instance.SetCompletionTask(taskIndex);
        for (int i = 0; i < toggle.Count; i++)
        {
            toggle[i].isOn = GameManager.Instance.GetIsCompletion(i);
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
        if(GameManager.Instance.GetCount() == Comletion)
        {
            return true;
        }

        return false;
    }
}