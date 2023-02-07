using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class TodayTask : MonoBehaviour
{
    
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
        //�O�̓������邩�ǂ����̊m�F
        if (GameManager.Instance.GetDate() - 1 > 0)
        {
            //�O���̏I����ĂȂ��^�X�N�̊m�F
            if (!GameManager.Instance.GetIsCompletion(GameManager.Instance.GetDate() - 1))
            {
                //�����z����^�X�N���̊m�F
                if (GameManager.Instance.GetTakeOver(GameManager.Instance.GetDate() - 1))
                {
                    todayTask.Add(GameManager.Instance.GetTaskName(GameManager.Instance.GetDate() - 1));
                }
            }
        }
        for (int i = 0; i < GameManager.Instance.GetCount(); i++)
        {            
            //�����̃^�X�N�̊m�F
            if (GameManager.Instance.GetTaskDate(i) == GameManager.Instance.GetDate())
                todayTask.Add(GameManager.Instance.GetTaskName(i));
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
            toggle[i].isOn = GameManager.Instance.GetIsCompletion(i);
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
            toggle[i].isOn = false;
            toggle[i].gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// �^�X�N�����������Ƃ��ɌĂ΂��
    /// </summary>
    public void TaskCompletion(int taskIndex)
    {
        Debug.Log("�^�X�N����");
        GameManager.Instance.SetCompletionTask(taskIndex);
        for (int i = 0; i < toggle.Count; i++)
        {
            toggle[i].isOn = GameManager.Instance.GetIsCompletion(i);
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
        if(GameManager.Instance.GetCount() == Comletion)
        {
            return true;
        }

        return false;
    }
}