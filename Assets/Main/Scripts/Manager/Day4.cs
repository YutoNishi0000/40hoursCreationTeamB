using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day4 : MonoBehaviour
{
    public static bool day4 = false;
    //�^�C�}�[
    float timer = 0;
    //�t�F�C�h�A�E�g����܂ł̎���
    float faidOutTime = 2;

    private Message message;
    private bool once;
    private TodayTask todayTask;

    private void Start()
    {
        message = GameObject.Find("MessageUI").GetComponent<Message>();
        todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
    }

    // Update is called once per frame
    void Update()
    {
        if (day4)
        {
            timer += Time.deltaTime;
            if (timer > faidOutTime)
            {
                GameManager.Instance.NextDay("Day 5_k");
                Destroy(gameObject);
            }
        }
        else if (TimeController._isTimePassed)
        {
            //�^�X�N����ł��c���Ă�����
            if (todayTask.todayTask.Count > 0)
            {
                //���Ԑ؂�ɂȂ�����Q�[���I�[�o�[
                if (!once)
                {
                    message.EventText((int)Scenario.MessageState.DAY4_TIMEOVER);
                    once = true;
                }

                //���̃t���O�͐�΂ɃI�t�ɂȂ�̂Ńo�O�͐S�z�����Ă悢
                if (Message.PlayerMoveFlag)
                {
                    GameManager.Instance.NextDay("FailedNegotiation");
                    Destroy(gameObject);
                }
            }
            //�^�X�N������c���Ă��Ȃ�������
            else
            {
                GameManager.Instance.NextDay("Negotiation");
                Destroy(gameObject);
            }

            //GameManager.Instance.NextDay("Day 5_k");
            //Destroy(gameObject);
        }
        else if (FrontSidePlayerChecker._Escaped)
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }
}
