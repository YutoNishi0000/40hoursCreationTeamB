using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ButtonGuide;

public class PoseUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>�{�^������ </summary>  
    [Header("�{�^������"), SerializeField]
    private ButtonGuide _buttonGuide;

    /// <summary>�{�^���̎�� </summary>  
    [Header("�{�^���̎��"), SerializeField]
    private ButtonType _buttonType;

    /// <summary>�I������Ă��鎞�̃{�^���摜 </summary>  
    [Header("�I������Ă��鎞�̃{�^���摜"), SerializeField]
    private Sprite _selectButton;

    /// <summary>�I������Ă��Ȃ����̃{�^���摜 </summary>  
    [Header("�I������Ă��Ȃ����̃{�^���摜"), SerializeField]
    private Sprite _noselectButton;

    /// <summary>�I�����{�^�� </summary>    
    private Button _button;

    /// <summary>�C���[�W </summary>    
    private Image _image;

    /// <summary>�e�L�X�g </summary>    
    private Text _text;

    /// <summary>�{�^���N���X���擾 </summary>
    public Button Button => _button;
    
    private void Start()
    {
        if(_buttonGuide == null || _selectButton == null || _noselectButton == null)
        {
            Debug.LogError("�C���X�y�N�^�[�Ɋi�[���Ă��������B");
            return;
        }
        _button = GetComponent<Button>();
        _image�@= GetComponent<Image>();
        _button.image.sprite = _noselectButton;

        // �I������Ă��Ȃ��Ƃ��̓{�^�����Â�����
        //Color disabledColor = Button.colors.disabledColor;
        //Button.image.color = disabledColor;
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
    /// <summary>�I������Ă�Ƃ��̓{�^���𖾂邭����</summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonGuide.ChangeText(_buttonType);
        _button.image.sprite = _selectButton;
    }
    /// <summary>�I������Ă��Ȃ��Ƃ��̓{�^�����Â�����</summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        _button.image.sprite = _noselectButton;
    }
}
