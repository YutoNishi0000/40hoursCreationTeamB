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
    //���ɂ�
    public int Date = 1;

    //�^�X�N�N���X    
    public List<TASK> tasks = new List<TASK>()
    {
        new TASK { taskName = "���̐l��������", isCompletion = false },
        new TASK { taskName = "���̐l��������", isCompletion = false },
        new TASK { taskName = "���̐l��������", isCompletion = false },
        new TASK { taskName = "���̐l��������", isCompletion = false },
        new TASK { taskName = "���̐l��������", isCompletion = false },
        new TASK { taskName = "���̐l��������", isCompletion = false },
        new TASK { taskName = "���̐l��������", isCompletion = false },
    };
    //�^�X�N���������Ă��邩
    [SerializeField]List<Toggle> toggle = new List<Toggle>();
    //�^�X�N��\������e�L�X�g
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
    /// �����̃^�X�N��\��
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