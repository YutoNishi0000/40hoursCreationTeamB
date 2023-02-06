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
    private GameObject Panel;   // UI��\��/��\��������
    private GameObject Player;   // �v���C���[�̃A�N�e�B�u��true/false������
    public int count = 0;       // �f�o�b�O�̂���public
    public int DayNumber = 0;   // �f�o�b�O�̂���public
    List<MessageData> TodayMes = null;
    [SerializeField] private Text text;
    [SerializeField] string DebugDayNumber = "�����ڂ̃��b�Z�[�W��\�����܂����H";

    // �e�L�X�g�C�x���g��false
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
        //�Q�[���X�^�[�g���A�v���C���[�̈ړ����s��
        PlayerMoveFlag = false;

        // �e�평����
        Init();

        Panel = transform.GetChild(0).gameObject;
        DayNumber = GetDayNamber();

        TodayMes = GetMesList(DayNumber);
    }

    private void Update()
    {

        if (!Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("������ĂȂ�");
            return;
        }

        if (TodayMes.Count > count)
        {
            if (GetMes(count).massage == "#")
            {
                count++;
                Panel.SetActive(false);
                //�v���C���[�̈ړ����\
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
        // GameManager���猻�݂�Day�ԍ��i0-4�j���擾
        return GameManager.Instance.GetDate();
    }
   
    // �f�o�b�O�p
    //public void DebugMessageDisplayDay1()
    //{
    //    Init();
    //    DayNumber = 0;
    //    TodayMes = GetMesList(DayNumber);
    //}
    // �f�o�b�O�p
    //public void DebugMessageDisplayDay2()
    //{
    //    Init();
    //    DayNumber = 1;
    //    TodayMes = GetMesList(DayNumber);
    //}
}