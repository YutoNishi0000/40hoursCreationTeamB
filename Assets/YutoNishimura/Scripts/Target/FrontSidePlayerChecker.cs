using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontSidePlayerChecker : Human
{
    [SerializeField] private float RECOGNIZE_DISTANCE = 10;       //�v���C���[��F���ł��鋗��
    [SerializeField] private Text timerText;
    [SerializeField] private float time;
    [SerializeField] private float RECOGNIZE_PLAYER_TIME = 3f;          //�^�[�Q�b�g���v���C���[��F������܂ł̎���
    private TargetController targetController;
    private bool _isEscape;                                       //�����������ǂ���
    private bool _isRecognizeBack;                                    //�v���C���[��F�����Ă��邩�ǂ���(����͈�)
    private bool _isEvent;                                      //���C�x���g�����ǂ���
    //public static bool _isEscape;
    public static bool _Escaped;                                        //�������ꂽ���ǂ���


    private void Start()
    {
        _isEscape = false;
        _isEvent = false;
        _isEscape = false;
        timerText.enabled = false;
        time = 0;
        targetController = GetComponentInParent<TargetController>();
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
            GetAway();
        }
    }

    public void OffTimer()
    {
        time = 0;
        timerText.enabled = false;
    }

    void GetAway()
    {
        //====================================================================
        //====================================================================
        //
        // �^�[�Q�b�g�����������Ƃ��̏���
        //
        //====================================================================
        //====================================================================

        if (!_isEscape)
        {
            targetController.SettargetState(TargetController.TargetState.LookPlayer);
        }

        if(_isEvent)
        {
            return;
        }

        Invoke(nameof(SetTargetStateEscape), 2);

        Debug.Log("�����C�x���g�����I");
    }

    void SetTargetStateEscape()
    {
        _isEscape = true;
        targetController.SettargetState(TargetController.TargetState.Escape);
        Invoke(nameof(GameOver), 1);
    }

    void GameOver()
    {
        _Escaped = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�v���Cy�|���m");
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

    public void TriggerEvent()
    {
        _isEvent = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isRecognizeBack = false;
        }
    }
}
