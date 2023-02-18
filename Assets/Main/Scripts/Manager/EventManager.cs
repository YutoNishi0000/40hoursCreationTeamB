using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g
    private GameObject player = null;
    //�^�[�Q�b�g�I�u�W�F�N�g
    private GameObject target = null;

    //�C���Q�[���C�x���g�̎��
    public enum EVENTTYPE
    {
        noEvent,        //�C�x���g�Ȃ�
        fled,           //������ꉉ�o
        handkerchief,   //�n���J�`�C�x���g
        negotiation,    //���C�x���g
    }
    public static EVENTTYPE eventType;


    //�����̃^�X�N�̏������Ă�I�u�W�F�N�g�ƃX�N���v�g
    GameObject task;
    TodayTask todayTask;
    private void Awake()
    {
        Message.PlayerMoveFlag = true;
    }
    private void Start()
    {
        task = GameObject.Find("TodayTask");
        todayTask = task.GetComponent<TodayTask>();
        player = GameObject.FindWithTag("Player");
        target = GameObject.FindWithTag("Target");
        eventType = EVENTTYPE.noEvent;
    }
    private void Update()
    {
        ////�C���Q�[���C�x���g���Ƃ̏���
        //switch(eventType)
        //{
        //    case EVENTTYPE.noEvent:
        //            break;
        //    case EVENTTYPE.fled:

        //        break;
        //    case EVENTTYPE.handkerchief:
        //        if (GameManager.Instance.GetInContactArea())
        //        {
        //            Debug.Log("InContactArea�̒�");
        //            HandkerchiefEvent();
        //        }
        //        break;
        //    case EVENTTYPE.negotiation:
        //        break;
        //}
        //switch (GameManager.Instance.GetDate())
        //{
        //    case 0:
        //        break;
        //    case 1:
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //    case 4:
        //        break;
        //}

    }
    /// <summary>
    /// �������C�x���g
    /// </summary>
    void FledEvent()
    {
        //GameManager.Instance.NextDay();
    }
    /// <summary>
    /// �n���J�`�C�x���g
    /// </summary>
    void HandkerchiefEvent()
    {
        if (GameManager.Instance.GetInContactArea())
        {
            Debug.Log("���������Ă�");
            //player.transform.LookAt(target.transform.position);
            //target.transform.LookAt(player.transform.position);
            //Message.PlayerMoveFlag = false;
        }
    }
    /// <summary>
    /// ���C�x���g
    /// </summary>
    void NegotiationEvent()
    {
        GameManager.Instance.GameClear();
    }
}
