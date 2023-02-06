using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
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
    private void Start()
    {
        task = GameObject.Find("TodayTask");
        todayTask = task.GetComponent<TodayTask>();
    }
    private void Update()
    {
        //�C���Q�[���C�x���g���Ƃ̏���
        switch(eventType)
        {
            case EVENTTYPE.noEvent:
                    break;
            case EVENTTYPE.fled:
                break;
            case EVENTTYPE.handkerchief:
                break;
            case EVENTTYPE.negotiation:
                break;
        }
    }
    /// <summary>
    /// �n���J�`�C�x���g
    /// </summary>
    void HandkerchiefEvent()
    {

    }
}
