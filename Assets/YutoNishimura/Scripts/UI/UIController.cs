using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

//UI�̊��N���X
//���̃N���X�͓��Ɍp���K�v���Ȃ��Ƃ��AUI���g�ɃA�^�b�`������̂Ƃ���
public class UIController : MonoBehaviour
{
    //�V�[���ړ�
    public void MoveScene(string scene) { SceneManager.LoadScene(scene); }

    public void PlaySE()
    {
        //�T�E���h��炷
    }
}
