using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class TodayTask : MonoBehaviour
{
    //日にち
    public int Date = 1;

    //タスクを表示するテキスト
    [SerializeField]Text[] taskText;

    //タスククラス
    public class TASK
    {
        private string taskName;
        private bool isCompletion;

        public TASK(string name, bool comp)
        {
            taskName = name;
            isCompletion = comp;
        }
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
    public TASK[] tasks = new TASK[7];

    private void Start()
    {
        tasks[0] = new TASK("あの人を見つける", false);
        tasks[1] = new TASK("あの人の写真を撮る", false);
        tasks[2] = new TASK("あの人に見つからない", false);
        tasks[3] = new TASK("ばれずに近寄る", false);
        tasks[4] = new TASK("ハンカチを返す", false);
        tasks[5] = new TASK("ばれずに近寄る", false);
        tasks[6] = new TASK("あの人に話しかける", false);
    }
    private void Update()
    {
        taskText[0].text = tasks[0].GetTaskName().ToString();
        taskText[1].text = tasks[1].GetTaskName().ToString();
        taskText[2].text = tasks[2].GetTaskName().ToString();
        taskText[3].text = tasks[3].GetTaskName().ToString();
        taskText[4].text = tasks[4].GetTaskName().ToString();
        //switch (Date)
        //{
        //    case 1:
        //        taskText[0].text = tasks[0].GetTaskName().ToString();
        //        taskText[1].text = tasks[1].GetTaskName().ToString();
        //        taskText[2].text = tasks[2].GetTaskName().ToString();
        //        taskText[3].text = tasks[3].GetTaskName().ToString();
        //        taskText[4].text = tasks[4].GetTaskName().ToString();
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //    case 4:
        //        break;
        //    case 5:
        //        break;
        //}
    }
    /// <summary>
    /// 今日のタスクを表示
    /// </summary>
    void DisplayTask()
    {

    }
}