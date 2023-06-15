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
        if(_buttonGuide == null)
        {
            Debug.LogError("�C���X�y�N�^�[�Ƀ{�^���K�C�h���i�[���Ă��������B");
            return;
        }
        _button = GetComponent<Button>();
        _image�@= GetComponent<Image>();
        _text = transform.GetChild(0).GetComponent<Text>();

        // �I������Ă��Ȃ��Ƃ��̓{�^�����Â�����
        Color disabledColor = Button.colors.disabledColor;
        Button.image.color = disabledColor;
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
    /// <summary>�I������Ă�Ƃ��̓{�^���𖾂邭���� </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonGuide.ChangeText(_buttonType);
        Color normalColor = Button.colors.normalColor;
        Button.image.color = normalColor;
    }
    /// <summary>�I������Ă��Ȃ��Ƃ��̓{�^�����Â����� </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        Color disabledColor = Button.colors.disabledColor;
        Button.image.color = disabledColor;
    }
}
