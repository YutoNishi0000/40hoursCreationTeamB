using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�v���C���[���@�m�͈͓��ɋ�����Q�[�W�����炵�čs��
public class PoliceController : MonoBehaviour
{
    private bool _policeCheckupFlag;            //�E���댯�@�m�t���O
    public Slider _dangerDetectionGauge;        //�댯�x�@�m�Q�[�W

    // Start is called before the first frame update
    void Start()
    {
        _policeCheckupFlag = false;

        if(_dangerDetectionGauge == null)
        {
            Debug.LogError("�댯�x�@�m�Q�[�W�����Ă�������");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _policeCheckupFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _policeCheckupFlag = false;
        }
    }
}
