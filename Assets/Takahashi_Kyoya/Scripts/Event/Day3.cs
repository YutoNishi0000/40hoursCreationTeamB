using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day3 : MonoBehaviour
{
    public static bool day3 = false;
    //�^�C�}�[
    float timer = 0;
    //�t�F�C�h�A�E�g����܂ł̎���
    float faidOutTime = 5;

    private TodayTask todayTask;
    private Message message;
    private bool once;

    private void Start()
    {
        once = false;
        todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
        message = GameObject.Find("MessageUI").GetComponent<Message>();
    }

    void Update()
    {
        if (day3)
        {
            timer += Time.deltaTime;
            if (timer > faidOutTime)
            {
                GameManager.Instance.NextDay("Day 4_k");
                Destroy(gameObject);
            }
        }
        else if (TimeController._isTimePassed)
        {
            //�^�X�N����ł��c���Ă�����
            if(todayTask.todayTask.Count > 0)
            {
                if (!once)
                {
                    message.EventText((int)Scenario.MessageState.DAY3_TIMEOVER);
                    once = true;
                }

                //���̃t���O�͐�΂ɃI�t�ɂȂ�̂Ńo�O�͐S�z�����Ă悢
                if (Message.PlayerMoveFlag)
                {
                    GameManager.Instance.GameOver();
                    Destroy(gameObject);
                }
            }
            else
            {
                GameManager.Instance.NextDay("Day 4_k");
                Destroy(gameObject);
            }
        }
    }
}
