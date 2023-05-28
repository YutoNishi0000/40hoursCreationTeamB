using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;


//UI�̊��N���X
//���̃N���X�͓��Ɍp���K�v���Ȃ��Ƃ��AUI���g�ɃA�^�b�`������̂Ƃ���
public class UIController : MonoBehaviour
{
    //true���ƃV�[���؂�ւ������Ȃ�
    public bool blockSwithScene = true;
    //�V�[���؂�ւ����̃A�j���[�V�����v���n�u�̃A�h���X
    private string address = "Assets/Kyoya_Takahashi/Prefabs/OutGame/Animation/SwichAnimationEnd.prefab";
    //�V�[���؂�ւ����̃A�j���[�V�����v���n�u
    public GameObject endAnimation = null;
    private void Awake()
    {
        endAnimation = AssetDatabase.LoadAssetAtPath<GameObject>(address);
    }
    //�V�[���ړ�
    public void MoveScene(string scene)
    {
        blockSwithScene = true;
        SceneManager.LoadScene(scene); 
    }
    public void PlaySE()
    {
        //�T�E���h��炷
    }
    public void UnBlockSwithScene()
    {
        blockSwithScene = false;
    }
}
