using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGuide : MonoBehaviour
{
    /// <summary>�e�L�X�g </summary>     
    private Text _text;

    /// <summary>�{�^���̐���[���X�^�[�g] </summary>  
    [Header("�{�^���̐���[���X�^�[�g]"), SerializeField]
    private string _restartText;

    /// <summary>�{�^���̐���[�I�v�V����] </summary>  
    [Header("�{�^���̐���[�I�v�V����]"), SerializeField]
    private string _optionText;

    /// <summary>�{�^���̐���[EXIT] </summary>  
    [Header("�{�^���̐���[EXIT]"), SerializeField]
    private string _exitText;

    /// <summary>�I������ </summary>  
    [Header("�I������"), SerializeField]
    private Image _selectItem;

    /// <summary>�I�����ڂɕ\������摜 </summary>  
    [Header("�I�����ڂɕ\������摜"), SerializeField]
    private Sprite[] _buttonSprites;

    /// <summary>�{�^���^�C�v </summary>
    public enum ButtonType
    {
        restart, option, exit
    }
    private void Start()
    {
        _text = transform.GetChild(0).GetComponent<Text>();
        ResetSelect();
    }
    /// <summary>�{�^��������ς��� </summary>
    public void ChangeText(ButtonType type)
    {
        _selectItem.color = Color.white;
        _selectItem.sprite = _buttonSprites[(int)type];

        switch (type)
        {
            case ButtonType.restart:
                _text.text = _restartText;
                break;
            case ButtonType.option:
                _text.text = _optionText;
                break;
            case ButtonType.exit:
                _text.text = _exitText;
                break;
            default:
                Debug.Log("�����p�e�L�X�g������܂���");
                break;
        }
    }
    /// <summary>�|�[�Y��ʂ�����Ƃ��ɌĂ΂�� </summary>
    public void ResetSelect()
    {
        _selectItem.sprite = null;
        _selectItem.color = Color.clear;
        _text.text = "";
    }
}
