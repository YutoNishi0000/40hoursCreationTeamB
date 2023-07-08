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

    /// <summary>�I�v�V�����{�^�� </summary>    
    [Header("�I�v�V�����{�^��"), SerializeField]
    private PoseUIManager _optionButton;

    /// <summary>�I�v�V������� </summary>    
    [Header("�I�v�V�������"), SerializeField]
    private GameObject _option;

    /// <summary>�I�v�V������EXIT�{�^�� </summary>  
    [Header("�I�v�V������EXIT�{�^��"), SerializeField]
    private HoverableButton _optionExitButton;

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
        if (_selectButton == null || _restartButton == null || _option == null) // || _optionButton == null)
        {
            Debug.LogError("selectUI�̃C���X�y�N�^�[��Button���i�[���Ă��������B");
            return;
        }

        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i] = transform.GetChild(i).gameObject;
        }
        _option.SetActive(false);
        _optionExitButton.AddOnClick(CloseOption);
        HideUI();

        // �{�^���N���b�N���ɃC�x���g�ǉ� ---------------------- //
        _selectButton. Button.onClick.AddListener(SelectMove);
        _restartButton.Button.onClick.AddListener(ReStartMove);
        _optionButton. Button.onClick.AddListener(OpenOption);
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
        BGMManager.Instance.BGMAdministrator(SelectSceneIndex);
        SceneManager.LoadScene(SelectSceneIndex);
        IsPosing = false;
    }
    /// <summary> ���X�^�[�g���� </summary>
    private void ReStartMove()
    {
        GameManager.Instance.IsPlayGame = false;
        IsPosing = false;
        SceneManager.LoadScene(ReStartSceneIndex);
        BGMManager.Instance.BGMAdministrator(ReStartSceneIndex);
    }
    /// <summary> �I�v�V������ʂ��J�� </summary>
    private void OpenOption()
    {
        _option.SetActive(true);
    }
    /// <summary> �I�v�V������ʂ���� </summary>
    private void CloseOption()
    {
        _option.SetActive(false);
        _optionExitButton.CancelHover();
    }
}
