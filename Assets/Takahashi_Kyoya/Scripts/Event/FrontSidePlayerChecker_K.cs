using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontSidePlayerChecker_K : Human
{
    [SerializeField] private float RECOGNIZE_DISTANCE = 10;       //�v���C���[��F���ł��鋗��
    [SerializeField] private Text timerText;
    [SerializeField] private float time;
    [SerializeField] private float RECOGNIZE_PLAYER_TIME = 3f;          //�^�[�Q�b�g���v���C���[��F������܂ł̎���
    private TargetController targetController;
    private bool _isEscape;                                       //�����������ǂ���
    private bool _isRecognizeBack;                                    //�v���C���[��F�����Ă��邩�ǂ���(����͈�)

    private void Start()
    {
        _isEscape = false;
        timerText.enabled = false;
        time = 0;
        targetController = GetComponentInParent<TargetController>();
    }

    private void Update()
    {
        if (/*_frontChecker.CheckPlayerFront() || */_isRecognizeBack)
        {
            GameManager.Instance.SetInContactArea(true);
            CountTimer();
        }
        else
        {
            GameManager.Instance.SetInContactArea(false);
            OffTimer();
        }
    }

    //�O����
    public bool CheckPlayerFront()
    {
        if (_isRecognizeBack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CountTimer()
    {
        timerText.enabled = true;
        time += Time.deltaTime;
        timerText.text = Mathf.FloorToInt(time) + "�b";

        //�^�[�Q�b�g���v���C���[��F���������̏���
        if(time >= RECOGNIZE_PLAYER_TIME)
        {
            Negotiation();
        }
    }

    public void OffTimer()
    {
        time = 0;
        timerText.enabled = false;
    }

    void Negotiation()
    {
        //====================================================================
        //====================================================================
        //
        // �^�[�Q�b�g�����������Ƃ��̏���
        //
        //====================================================================
        //====================================================================

        Debug.Log("���C�x���g�I");
        Day5.day5 = true;
    }

    void SetTargetStateEscape()
    {
        _isEscape = true;
        targetController.SettargetState(TargetController.TargetState.Escape);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //���C���v���C���[�̕����ɔ�΂��ĉ���������Ȃ�������
            if (Physics.Raycast(transform.position, other.gameObject.transform.position - transform.position, 10f, LayerMask.GetMask("Player")))
            {
                _isRecognizeBack = true;
            }
            else
            {
                _isRecognizeBack = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isRecognizeBack = false;
        }
    }
}
