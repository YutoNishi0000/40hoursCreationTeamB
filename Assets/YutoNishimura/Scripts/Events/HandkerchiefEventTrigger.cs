using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventTrigger : MonoBehaviour
{
    private TodayTask _todayTask;
    private Message _message;
    private bool _hankatiEventStart;
    private ChangeCameraAngle changeCamera;

    private void Start()
    {
        _hankatiEventStart = false;
        changeCamera = GetComponent<ChangeCameraAngle>();
        _todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
        _message = GameObject.Find("MessageUI").GetComponent<Message>();
    }

    private void Update()
    {
        if(!Message.TextEventFlag && _hankatiEventStart)
        {
            Day3.day3 = true;
            _hankatiEventStart = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Handkerchief"))
        {
            HandkerchiefEvent();
        }
    }

    /// <summary>
    /// �n���J�`���E�����Ƃ��ɏ�������֐�
    /// </summary>
    void HandkerchiefEvent()
    {
        //======================================================================
        //======================================================================
        //
        // �����Ƀn���J�`���E�����Ƃ��̏������L�q���Ă�������
        //
        //======================================================================
        //======================================================================

        //�C�x���g���̃e�L�X�g��\��
        _message.EventText((int)Scenario.MessageState.DAY3_GET);

        //�C�x���g���ǂꂮ�炢�B������Ă��邩�`�F�b�N
        for(int i = 0; i < _todayTask.todayTask.Count; i++)
        {
            //�������^�X�N��Day3�̃^�X�N�ł����
            if (GameManager.Instance.tasks[i].date == 2)
            {
                //���̃^�X�N���N���A���ꂽ��Ԃɂ���
                GameManager.Instance.tasks[i].isCompletion = true;
            }
        }

        int j = 0;

        if (_todayTask.todayTask.Count == 2 && GameManager.Instance.tasks[j].isCompletion && GameManager.Instance.tasks[j + 1].isCompletion)
        {
            //�����ł����S�Ẵ^�X�N���N���A����Ă�����
            //����Day�Ɉڍs
            _hankatiEventStart = true;
        }
        //�^�X�N�̐������
        else if (_todayTask.todayTask.Count == 1 && GameManager.Instance.tasks[j].isCompletion)
        {
            //�����ł����S�Ẵ^�X�N���N���A����Ă�����
            //����Day�Ɉڍs
            _hankatiEventStart = true;
        }
        //�ǂ̏����ɂ����Ă͂܂炸�A�e�L�X�g�\�����I�����Ă�����
        else if(!Message.PlayerMoveFlag)
        {
            //�v���C���[�̌��̎��_�ɖ߂�
            changeCamera.ExitVoyeurism();
        }

        //_todayTask.TaskCompletion(2);
        UIController._talkStart = false;
    }
}
