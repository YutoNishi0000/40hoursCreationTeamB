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
        Debug.Log("�V�[���؂�ւ��J�n");
        GameManager.Instance.blockSwithScene = true;
        SceneManager.LoadScene(scene); 
    }
    public void PlaySE()
    {
        //�T�E���h��炷
    }
    public void UnBlockSwithScene()
    {
        Debug.Log("�Ă΂ꂽ");
        GameManager.Instance.blockSwithScene = false;
    }
    public void InstantAnimation()
    {
        Instantiate(endAnimation);
    }
}
