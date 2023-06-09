using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectUI : MonoBehaviour
{
    /// <summary>�|�[�Y�����ǂ��� </summary>
    public bool _isPosing = false;

    /// <summary>�w�i </summary>    
    [Header("�w�i"), SerializeField]
    private PoseUIManager _background;

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
    private const int SelectSceneIndex = 2;

    /// <summary>�Q�[���V�[���̃C���f�b�N�X�ԍ�</summary>
    private const int ReStartSceneIndex = 3;
    
    /// <summary>�I�v�V�����V�[���̃C���f�b�N�X�ԍ�</summary>
    private const int OptionSceneIndex = 6;

    void Start()
    {
        if (_selectButton == null || _reStartButton == null || _optionButton == null)
        {
            Debug.LogError("selectUI�̃C���X�y�N�^�[��Button���i�[���Ă��������B");
            return;
        }

        _background.HideUI();
        _selectButton.HideUI();
        _reStartButton.HideUI();
        _optionButton.HideUI();
        //HidePoseUI();

        // �{�^���N���b�N���ɃC�x���g�ǉ� ---------------------- //
        _selectButton. Button.onClick.AddListener(SelectMove);
        _reStartButton.Button.onClick.AddListener(ReStartMove);
        _optionButton. Button.onClick.AddListener(OptionMove);
        // ----------------------------------------------------- //
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isPosing = true;
            GameManager.Instance.IsPlayGame = false;
            ShowPoseUI();
        }
    }

    /// <summary> �|�[�Y��ʔ�\�� </summary>
    private void HidePoseUI()
    {
        
    }
    /// <summary> �|�[�Y��ʕ\�� </summary>
    private void ShowPoseUI()
    {
        _background.ShowUI();
        _selectButton.ShowUI();
        _reStartButton.ShowUI();
        _optionButton.ShowUI();
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
