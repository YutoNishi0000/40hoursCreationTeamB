using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>�z�o�[�\�ȃ{�^���N���X </summary>
[RequireComponent(typeof(Button))]
public class HoverableButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>�I������Ă��鎞�̃{�^���摜 </summary>  
    [Header("�I������Ă��鎞�̃{�^���摜"), SerializeField]
    private Sprite _selectButton;

    /// <summary>�I������Ă��Ȃ����̃{�^���摜 </summary>  
    [Header("�I������Ă��Ȃ����̃{�^���摜"), SerializeField]
    private Sprite _noSelectButton;

    /// <summary> �{�^�� </summary>
    private Button _button;

    /// <summary>�z�o�[�J�n���ɌĂ΂��C�x���g</summary>
    private readonly List<UnityAction> _OnPointerEnter = new List<UnityAction>();

    /// <summary> �z�o�[�J�n���̃C�x���g�ǉ� </summary>
    public void AddOnPointerEnter(UnityAction action) => _OnPointerEnter.Add(action);

    /// <summary> �z�o�[�J�n���̃C�x���g���O </summary>
    public void RemoveOnPointerEnter(UnityAction action) => _OnPointerEnter.Remove(action);
    void Start()
    {
        _button = GetComponent<Button>();
    }

    /// <summary>�I������Ă�Ƃ��̓{�^���𖾂邭����</summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        ActiveHover();
        for(int i = 0; i < _OnPointerEnter.Count; i++)
        {
            _OnPointerEnter[i].Invoke();
        }
    }
    /// <summary>�I������Ă��Ȃ��Ƃ��̓{�^�����Â�����</summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        CancelHover();
    }
    /// <summary> �{�^���N���b�N���ɃC�x���g�ǉ� </summary>
    public void AddOnClick(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
    
    /// <summary>�z�o�[������ </summary>
    public void ActiveHover()
    {
        _button.image.sprite = _selectButton;
    }
    /// <summary>�z�o�[����߂� </summary>
    public void CancelHover()
    {
        _button.image.sprite = _noSelectButton;
    }
    private void OnDestroy()
    {
        _OnPointerEnter.Clear();
    }
}
