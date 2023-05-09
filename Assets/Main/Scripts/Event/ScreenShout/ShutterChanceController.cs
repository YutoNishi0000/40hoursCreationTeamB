using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�V���b�^�[�`�����X���ǂ������Ǘ�����N���X
public class ShutterChanceController : MonoBehaviour
{
    public static bool _shutterChance;      //�V���b�^�[�`�����X���ǂ������Ǘ�����t���O

    // Start is called before the first frame update
    void Start()
    {
        _shutterChance = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_shutterChance)
        {
            Debug.Log("�V���b�^�[�`�����X");
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Invoke("Good", 0.9f);
            }
        }
        else
        {
            //Debug.Log("�̂��ƃV���b�^�[�`�����X");
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Invoke("Bad", 0.9f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�������V���b�^�[�`�����X�G���A�Ƀ^�[�Q�b�g���i��������
        if(other.gameObject.CompareTag("ShutterChanceArea"))
        {
            //�t���O���I����
            _shutterChance = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //�������V���b�^�[�`�����X�G���A����^�[�Q�b�g���o����
        if (other.gameObject.CompareTag("ShutterChanceArea"))
        {
            //�t���O���I�t��
            _shutterChance = false;
        }
    }
    void Good()
    {
        SoundManager.Instance.PlayGoodSE();
    }
    void Bad()
    {
        SoundManager.Instance.PlayBadSE();
    }

}
