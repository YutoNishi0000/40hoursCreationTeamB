using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI�̕\����\�����s�����߂̃N���X
public class UIController : MonoBehaviour
{
    public static bool _talkStart;

    public Image[] OffImage;

    public Text[] OffTexts;

    private PlayerStateController _playerState;

    // Start is called before the first frame update
    void Start()
    {
        _playerState = GetComponent<PlayerStateController>();
        _talkStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�X�e�[�g���g�[�N��Ԃ�������
        if(_playerState.GetPlayerState() == PlayerStateController.PlayerState.TalkEvent)
        {
            Debug.Log("UI��\��");
            OffUI(OffImage, OffTexts);
        }
        //�g�[�N��ԈȊO��������
        else
        {
            Debug.Log("UI�\��");
            OnUI(OffImage, OffTexts);
        }
    }

    public void OffUI(Image[] images, Text[] texts)
    {
        for(int i = 0; i < images.Length; i++)
        {
            if (images == null)
            {
                break;
            }

            //�z��ɓ����Ă���S�ẴC���[�WUI�A�e�L�X�gUI���\���ɂ���
            images[i].enabled = false;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts == null)
            {
                break;
            }

            //�z��ɓ����Ă���S�ẴC���[�WUI�A�e�L�X�gUI���\���ɂ���
            images[i].enabled = false;
        }
    }

    public void OnUI(Image[] images, Text[] texts)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images == null)
            {
                break;
            }

            //�z��ɓ����Ă���S�ẴC���[�WUI�A�e�L�X�gUI���\���ɂ���
            images[i].enabled = true;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts == null)
            {
                break;
            }

            //�z��ɓ����Ă���S�ẴC���[�WUI�A�e�L�X�gUI���\���ɂ���
            images[i].enabled = true;
        }
    }
}
