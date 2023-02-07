using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TASK
{
    //�\������^�X�N�e�L�X�g
    public string taskName;
    //�^�X�N���������Ă��邩
    public bool isCompletion;
    //���̓��Ɏ����z���邩
    public bool takeOver;
    //�����ڂ̃^�X�N��
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
    //�^�X�N�N���X
    List<TASK> tasks = new List<TASK>()
    {
        //day1
        new TASK { taskName = "�h���̐l�h�������悤\n(�ł�������ȁI)",
            isCompletion = false, takeOver = true , date = 0 },
        //day2
        new TASK { taskName = "�΂�Ȃ悤�Ɏʐ^��\n�B�낤",                 
            isCompletion = false, takeOver = true , date = 1 },
        //day3
        new TASK { taskName = "�H�H�H\n(�Ƃ肠�����h���̐l�h��T����)", 
            isCompletion = false, takeOver = false, date = 2 },
        //day4
        new TASK { taskName = "�n���J�`��Ԃ���",                       
            isCompletion = false, takeOver = false, date = 3 },
        //day5
        new TASK { taskName = "�h���̐l�h�ɘb�������悤",               
            isCompletion = false, takeOver = false, date = 4 },
    };
    //�^�X�N���������Ă��邩��UI
    [SerializeField] List<Toggle> toggle = new List<Toggle>();
    //�^�X�N��\������e�L�X�gUI
    [SerializeField] List<Text> taskText = new List<Text>();

    //�����̃^�X�N
    List<string> todayTask = new List<string>();

    private void Start()
    {
        Initialize();
        CheckTodayTask();
        DisplayTask();
    }
    /// <summary>
    /// �����̃^�X�N�̊m�F
    /// </summary>
    void CheckTodayTask()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            //�O�̓������邩�ǂ����̊m�F
            if (GameManager.Instance.GetDate() - 1 > 0)
            {
                //�O���̏I����ĂȂ��^�X�N�̊m�F
                if (GameManager.Instance.GetDate() - 1 == tasks[i].date)
                {
                    //�����z����^�X�N���̊m�F
                    if (tasks[i].takeOver == true)
                    {
                        //�I����Ă邩�̊m�F
                        if (tasks[i].isCompletion == false)
                            todayTask.Add(tasks[i].taskName);
                    }
                }
            }
            //�����̃^�X�N�̊m�F
            if (GameManager.Instance.GetDate() == tasks[i].date)
                todayTask.Add(tasks[i].taskName);
        }
    }
    /// <summary>
    /// �����̃^�X�N��\��
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
    /// ������
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
    /// �^�X�N�����������Ƃ��ɌĂ΂��
    /// </summary>
    public void TaskCompletion(int taskIndex)
    {
        Debug.Log("�^�X�N����");
        tasks[taskIndex].CompletionTask();
        for (int i = 0; i < todayTask.Count; i++)
        {
            toggle[i].isOn = tasks[i].GetIsCompletion();
        }
    }
    /// <summary>
    /// �^�X�N�����ׂďI����Ă��邩�̊m�F
    /// </summary>
    /// <returns>�^�X�N�����ׂďI����Ă���true ���ׂďI����ĂȂ�������false</returns>
    public bool IsAllTaskComletion()
    {
        int Comletion = 0;
        //�^�X�N�����ׂďI����Ă��邩
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