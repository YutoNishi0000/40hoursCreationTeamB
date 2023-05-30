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
    protected GameObject endAnimation = null;

   
    private const int stageSlectIndex = 1;  //�X�e�[�W�I���V�[���̃C���f�b�N�X�ԍ�
    private const int homeIndex = 2;        //�z�[���V�[���̃C���f�b�N�X�ԍ�
    private const int stageIndex = 3;       //�X�e�[�W�V�[���̃C���f�b�N�X�ԍ�
    private const int resultIndex = 4;      //���U���g�V�[���̃C���f�b�N�X�ԍ�
    private const int operationIndex = 5;   //�I�y���[�V�����V�[���̃C���f�b�N�X�ԍ�

    private void Awake()
    {
#if UNITY_EDITOR
        endAnimation = AssetDatabase.LoadAssetAtPath<GameObject>(address);
#endif
    }
    //�V�[���ړ�
    public void MoveScene(int index)
    {
        if (!GameManager.Instance.blockSwithScene)
        {
            GameManager.Instance.GameAdministrator();
            SceneManager.LoadScene(index);
        }
        GameManager.Instance.blockSwithScene = true;         
    }
    public void PlaySelectSE()
    {
        SEManager.Instance.PlaySelect();
    }
    public void PlayDecisionSE()
    {
        SEManager.Instance.PlayDecision();
    }
    public void PlayBackSE()
    {
        SEManager.Instance.PlayBack();
    }
    public void UnBlockSwithScene()
    {
        GameManager.Instance.blockSwithScene = false;
    }
    public void InstantAnimation()
    {
        Instantiate(GameManager.Instance.animations[(int)GameManager.ShutterAnimationState.End]);
    }
    
    public void StageSlectScene()
    {
        GameManager.Instance.sceneIndex = homeIndex;
    }
    public void HomeScene()
    {
        GameManager.Instance.sceneIndex = stageSlectIndex;
    }
    public void StageScene()
    {
        GameManager.Instance.sceneIndex = stageIndex;
    }
    public void ResultScene()
    {
        Debug.Log("���U���g");
        GameManager.Instance.sceneIndex = resultIndex;
    }
    public void OperationScene()
    {
        GameManager.Instance.sceneIndex = operationIndex;
    }
}
