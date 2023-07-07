using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pose : MonoBehaviour
{
    /// <summary>�Z���N�g�{�^�� </summary>    
    [Header("�Z���N�g�{�^��"), SerializeField]
    private PoseUIManager _selectButton;

    /// <summary>���X�^�[�g�{�^�� </summary>    
    [Header("���X�^�[�g�{�^��"), SerializeField]
    private PoseUIManager _restartButton;

    ///// <summary>�I�v�V�����{�^�� </summary>    
    //[Header("�I�v�V�����{�^��"), SerializeField]
    //private PoseUIManager _optionButton;

    /// <summary>�Z���N�g�V�[���̃C���f�b�N�X�ԍ�</summary>
    [Header("�Z���N�g�V�[���̃C���f�b�N�X�ԍ�"), SerializeField]
    private int SelectSceneIndex;

    /// <summary>�Q�[���V�[���̃C���f�b�N�X�ԍ�</summary>
    [Header("�Q�[���V�[���̃C���f�b�N�X�ԍ�"), SerializeField]
    private int ReStartSceneIndex;

    ///// <summary>�I�v�V�����V�[���̃C���f�b�N�X�ԍ�</summary>
    //[Header("�I�v�V�����V�[���̃C���f�b�N�X�ԍ�"), SerializeField]
    //private int OptionSceneIndex;

    /// <summary>�{�^������ </summary>  
    [Header("�{�^������"), SerializeField]
    private ButtonGuide _buttonGuide;

    //�q�I�u�W�F�N�g�̐�
    private const int childNum = 8;   
    
    //UI�̃I�u�W�F�N�g���
    private GameObject[] poseUI = new GameObject[childNum];

    //�|�[�Y�����ǂ���
    public static bool IsPosing = false;

    private void Start()
    {
        if (_selectButton == null || _restartButton == null) // || _optionButton == null)
        {
            Debug.LogError("selectUI�̃C���X�y�N�^�[��Button���i�[���Ă��������B");
            return;
        }

        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i] = transform.GetChild(i).gameObject;
        }

        HideUI();

        // �{�^���N���b�N���ɃC�x���g�ǉ� ---------------------- //
        _selectButton. Button.onClick.AddListener(SelectMove);
        _restartButton.Button.onClick.AddListener(ReStartMove);
        //_optionButton. Button.onClick.AddListener(OptionMove);
        // ----------------------------------------------------- //
    }

    private void Update()
    {
        // ===== �|�[�Y���̏��� =====
        if (IsPosing)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                HideUI();
                // �J�[�\����\��
                Cursor.lockState = CursorLockMode.Locked;
                GameManager.Instance.IsPlayGame = true;
                IsPosing = false;
                _buttonGuide.ResetSelect();
            }
            return;
        }
        // ===== �|�[�Y����Ȃ��Ƃ��̏��� =====
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ShowUI();
            // �J�[�\����\��
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.IsPlayGame = false;
            IsPosing = true;
        }
    }
    /// <summary> �|�[�Y��ʔ�\�� </summary>
    private void HideUI()
    {
        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i].SetActive(false);
        }
    }
    /// <summary> �|�[�Y��ʕ\�� </summary>
    private void ShowUI()
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
        GameManager.Instance.IsPlayGame = false;
        SceneManager.LoadScene(ReStartSceneIndex);
    }
    /// <summary> �I�v�V������ʂɈړ� </summary>
    private void OptionMove()
    {
        SceneManager.LoadScene(OptionSceneIndex);
    }
}
