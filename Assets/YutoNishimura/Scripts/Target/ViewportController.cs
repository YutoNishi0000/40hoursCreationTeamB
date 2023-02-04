using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewportController : Human
{
    [SerializeField] private float RECOGNIZE_DISTANCE = 10;       //�v���C���[��F���ł��鋗��
    [SerializeField] private Text timerText;
    [SerializeField] private float time;

    private void Start()
    {
        timerText.enabled = false;
        time = 0;
    }

    void Update()
    {
        CheckPlayer();
    }

    void CheckPlayer()
    {
        //�^�[�Q�b�g����J�����̕����֐��K�������x�N�g�����쐬
        Vector3 targetToCameraDirection_N = (transform.position - playerInstance.gameObject.transform.position).normalized;

        //���K�������x�N�g���̓��ς����ȉ��Ȃ猩�����Ƃɂ���
        if (Vector3.Dot(targetToCameraDirection_N, transform.forward.normalized) < -0.5 && Vector3.Distance(transform.position, playerInstance.gameObject.transform.position) <= RECOGNIZE_DISTANCE)
        {
            print("�����I");

            //==========================================================================
            // �����Ŏ��Ԍv���┻����s��
            //==========================================================================
            CountTimer();
        }
        else
        {
            OffTimer();
        }
    }

    void CountTimer()
    {
        timerText.enabled = true;
        time += Time.deltaTime;
        timerText.text = Mathf.FloorToInt(time) + "�b";
    }

    void OffTimer()
    {
        time = 0;
        timerText.enabled = false;
    }
}
