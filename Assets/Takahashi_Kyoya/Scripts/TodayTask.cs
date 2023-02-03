using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TASK
{
    public string taskName;
    public bool isCompletion;
    //private bool takeOver;

    //public TASK(string name, bool comp)
    //{
    //    taskName = name;
    //    isCompletion = comp;
    //}
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
    //日にち
    public int Date = 1;

    //タスククラス    
    public List<TASK> tasks = new List<TASK>()
    {
        new TASK { taskName = "あの人を見つける", isCompletion = false },
        new TASK { taskName = "あの人を見つける", isCompletion = false },
        new TASK { taskName = "あの人を見つける", isCompletion = false },
        new TASK { taskName = "あの人を見つける", isCompletion = false },
        new TASK { taskName = "あの人を見つける", isCompletion = false },
        new TASK { taskName = "あの人を見つける", isCompletion = false },
        new TASK { taskName = "あの人を見つける", isCompletion = false },
    };
    //タスクが完了しているか
    [SerializeField]List<Toggle> toggle = new List<Toggle>();
    //タスクを表示するテキスト
    [SerializeField] List<Text> taskText = new List<Text>();

    private void Start()
    {
        
    }
    private void Update()
    {
        DisplayTask();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].CompletionTask();
            }
        }
    }
    /// <summary>
    /// 今日のタスクを表示
    /// </summary>
    void DisplayTask()
    {
        for(int i = 0; i < taskText.Count; i++)
        {
            taskText[i].text = tasks[i].GetTaskName().ToString();
            toggle[i].isOn = tasks[i].GetIsCompletion();
        }
    }
}