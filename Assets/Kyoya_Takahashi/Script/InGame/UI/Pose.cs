using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pose : MonoBehaviour
{
    /// <summary>�Z���N�g�{�^�� </summary>    
    [Header("�Z���N�g�{�^��"), SerializeField]
    private PoseUIManager _selectButton;

    /// <summary>���X�^�[�g�{�^�� </summary>    
    [Header("���X�^�[�g�{�^��"), SerializeField]
    private PoseUIManager _reStartButton;

    /// <summary>�I�v�V�����{�^�� </summary>    
    [Header("�I�v�V�����{�^��"), SerializeField]
    private PoseUIManager _optionButton;

    /// <summary>�Z���N�g�V�[���̃C���f�b�N�X�ԍ�</summary>
    [Header("�Z���N�g�V�[���̃C���f�b�N�X�ԍ�"), SerializeField]
    private int SelectSceneIndex;

    /// <summary>�Q�[���V�[���̃C���f�b�N�X�ԍ�</summary>
    [Header("�Q�[���V�[���̃C���f�b�N�X�ԍ�"), SerializeField]
    private int ReStartSceneIndex;

    /// <summary>�I�v�V�����V�[���̃C���f�b�N�X�ԍ�</summary>
    [Header("�I�v�V�����V�[���̃C���f�b�N�X�ԍ�"), SerializeField]
    private int OptionSceneIndex;

    //�q�I�u�W�F�N�g�̐�
    private const int childNum = 4;   
    
    //UI�̃I�u�W�F�N�g���
    private GameObject[] poseUI = new GameObject[childNum];

    //�|�[�Y�����ǂ���
    private bool IsPosing = false;

    private void Start()
    {
        if (_selectButton == null || _reStartButton == null || _optionButton == null)
        {
            Debug.LogError("selectUI�̃C���X�y�N�^�[��Button���i�[���Ă��������B");
            return;
        }

        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i] = transform.GetChild(i).gameObject;
        }

        hideUI();

        // �{�^���N���b�N���ɃC�x���g�ǉ� ---------------------- //
        _selectButton.Button.onClick.AddListener(SelectMove);
        _reStartButton.Button.onClick.AddListener(ReStartMove);
        _optionButton.Button.onClick.AddListener(OptionMove);
        // ----------------------------------------------------- //
    }

    private void Update()
    {
        // ===== �|�[�Y���̏��� =====
        if (IsPosing)
        {
            showUI();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsPosing = false;
            }
            return;
        }
        // ===== �|�[�Y����Ȃ��Ƃ��̏��� =====
        hideUI();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsPosing = true;
        }
    }
    /// <summary> �|�[�Y��ʔ�\�� </summary>
    private void hideUI()
    {
        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i].SetActive(false);
        }
    }
    /// <summary> �|�[�Y��ʕ\�� </summary>
    private void showUI()
    {
        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i].SetActive(true);
        }
    }
    /// <summary> �Z���N�g��ʂɈړ� </summary>
    private void SelectMove()
    {
        SceneManager.LoadScene(SelectSceneIndex);
    }
    /// <summary> ���X�^�[�g���� </summary>
    private void ReStartMove()
    {
        SceneManager.LoadScene(ReStartSceneIndex);
    }
    /// <summary> �I�v�V������ʂɈړ� </summary>
    private void OptionMove()
    {
        SceneManager.LoadScene(OptionSceneIndex);
    }
}
