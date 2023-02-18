using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�v���C���[���@�m�͈͓��ɋ�����Q�[�W�����炵�čs��
public class PoliceController : MonoBehaviour
{
    private static bool _policeCheckupFlag;            //�E���댯�@�m�t���O
    public Slider[] _dangerDetectionGauge;        //�댯�x�@�m�Q�[�W
    private readonly float CAUGHT_TIME = 4;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        for(int i = 0; i < _dangerDetectionGauge.Length; i++)
        {
            _dangerDetectionGauge[i].maxValue = CAUGHT_TIME;
        }

        _policeCheckupFlag = false;

        if(_dangerDetectionGauge == null)
        {
            Debug.LogError("�댯�x�@�m�Q�[�W�����Ă�������");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�E���댯�@�m�t���O���I���������ꍇ��
        if (_policeCheckupFlag)
        {
            Debug.Log("�v���C���[��E�����悤���ȁE�E�E");
            for (int i = 0; i < _dangerDetectionGauge.Length; i++)
            {
                _dangerDetectionGauge[i].value -= Time.deltaTime;

                //�������X���C�_�[�̒l���O��菬�����Ȃ�����
                if(_dangerDetectionGauge[i].value <= 0)
                {
                    Debug.Log("�E���J�n�I�I");
                    PoliceEvent();
                }
            }
        }
        //�E���댯�@�m�t���O���I�t�������ꍇ��
        else
        {
            //�Q�[�W�̒l�����Ƃɖ߂�
            for (int i = 0; i < _dangerDetectionGauge.Length; i++)
            {
                _dangerDetectionGauge[i].DOValue(_dangerDetectionGauge[i].maxValue, CAUGHT_TIME / 3);
            }
        }
    }

    /// <summary>
    /// �x���ɐE�����ꂽ���̃C�x���g
    /// </summary>
    void PoliceEvent()
    {
        //====================================================================================
        //====================================================================================
        //
        // �����Ɍx������E�����ꂽ�ۂ̏������L�q���Ă�������
        //
        //====================================================================================
        //====================================================================================
    }

    private void OnTriggerEnter(Collider other)
    {
        //�������v���C���[���@�m�͈͓��ɐi�����Ă�����
        if(other.gameObject.CompareTag("Player"))
        {
            //�t���O���I���ɂ���
            _policeCheckupFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //�������v���C���[���@�m�͈͓��ɐi�����Ă�����
        if (other.gameObject.CompareTag("Player"))
        {
            //�t���O���I�t�ɂ���
            _policeCheckupFlag = false;
        }
    }
}
