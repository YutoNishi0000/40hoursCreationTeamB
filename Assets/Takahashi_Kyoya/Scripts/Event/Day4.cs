using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Day4 : MonoBehaviour
{
    public static bool day4 = false;
    //�^�C�}�[
    float timer = 0;
    //�t�F�C�h�A�E�g����܂ł̎���
    float faidOutTime = 5;

    private Message message;

    private void Start()
    {
        message = GameObject.Find("MessageUI").GetComponent<Message>();
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
            //���Ԑ؂�ɂȂ�����Q�[���I�[�o�[

            message.EventText((int)Scenario.MessageState.DAY4_TIMEOVER);

            //���̃t���O�͐�΂ɃI�t�ɂȂ�̂Ńo�O�͐S�z�����Ă悢
            if (Message.PlayerMoveFlag)
            {
                GameManager.Instance.GameOver();
                Destroy(gameObject);
            }

            //GameManager.Instance.NextDay("Day 5_k");
            //Destroy(gameObject);
        }
    }
}
