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

    private void Start()
    {
        _isEscape = false;
        timerText.enabled = false;
        time = 0;
        targetController = GetComponent<TargetController>();
    }

    //�O����
    public bool CheckPlayerFront()
    {
        RaycastHit hit;
        if (Physics.BoxCast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Vector3(2, 2, 4), transform.forward, out hit, Quaternion.identity, 10f, LayerMask.GetMask("Player")))
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

        Invoke(nameof(SetTargetStateEscape), 2);

        Debug.Log("�����C�x���g�����I");
    }

    void SetTargetStateEscape()
    {
        _isEscape = true;
        targetController.SettargetState(TargetController.TargetState.Escape);
    }
}
