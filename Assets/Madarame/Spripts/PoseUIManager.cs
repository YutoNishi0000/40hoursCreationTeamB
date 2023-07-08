using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ButtonGuide;

public class PoseUIManager : MonoBehaviour
{
    /// <summary>�{�^������ </summary>  
    [Header("�{�^������"), SerializeField]
    private ButtonGuide _buttonGuide;

    /// <summary>�{�^���̎�� </summary>  
    [Header("�{�^���̎��"), SerializeField]
    private ButtonType _buttonType;

    /// <summary>�I�v�V�����{�^�� </summary>  
    [Header("�I�v�V�����{�^��"), SerializeField]
    private HoverableButton _optionButton;

    /// <summary>�I�����{�^�� </summary>    
    private Button _button;

    /// <summary>�C���[�W </summary>    
    private Image _image;

    /// <summary>�e�L�X�g </summary>    
    private Text _text = null;

    /// <summary>�{�^���N���X���擾 </summary>
    public Button Button => _button;

    /// <summary>�{�^���̃e�L�X�g�ύX</summary>
    private void ChangeText() => _buttonGuide.ChangeText(_buttonType);

    private void Start()
    {
        if(_buttonGuide == null || _optionButton == null)
        {
            Debug.LogError("�C���X�y�N�^�[�Ɋi�[���Ă��������B");
            return;
        }
        _button = GetComponent<Button>();
        _image�@= GetComponent<Image>();

        _optionButton.CancelHover();
        
        _optionButton.AddOnPointerEnter(ChangeText);
    }
    /// <summary>UI�\��</summary>
    public void ShowUI()
    {
        _image.enabled = true;
        _text.enabled = true;
    }
    /// <summary>UI��\��</summary>
    public void HideUI()
    {
        _image.enabled = false;
        _text.enabled = false;
    }

}