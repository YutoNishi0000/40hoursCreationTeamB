using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>ホバー可能なボタンクラス </summary>
[RequireComponent(typeof(Button))]
public class HoverableButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>選択されている時のボタン画像 </summary>  
    [Header("選択されている時のボタン画像"), SerializeField]
    private Sprite _selectButton;

    /// <summary>選択されていない時のボタン画像 </summary>  
    [Header("選択されていない時のボタン画像"), SerializeField]
    private Sprite _noSelectButton;

    /// <summary> ボタン </summary>
    private Button _button;

    /// <summary>ホバー開始時に呼ばれるイベント</summary>
    private readonly List<UnityAction> _OnPointerEnter = new List<UnityAction>();

    /// <summary> ホバー開始時のイベント追加 </summary>
    public void AddOnPointerEnter(UnityAction action) => _OnPointerEnter.Add(action);

    /// <summary> ホバー開始時のイベント除外 </summary>
    public void RemoveOnPointerEnter(UnityAction action) => _OnPointerEnter.Remove(action);
    void Start()
    {
        _button = GetComponent<Button>();
    }

    /// <summary>選択されてるときはボタンを明るくする</summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        ActiveHover();
        for(int i = 0; i < _OnPointerEnter.Count; i++)
        {
            _OnPointerEnter[i].Invoke();
        }
    }
    /// <summary>選択されていないときはボタンを暗くする</summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        CancelHover();
    }
    /// <summary> ボタンクリック時にイベント追加 </summary>
    public void AddOnClick(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
    
    /// <summary>ホバーをする </summary>
    public void ActiveHover()
    {
        _button.image.sprite = _selectButton;
    }
    /// <summary>ホバーをやめる </summary>
    public void CancelHover()
    {
        _button.image.sprite = _noSelectButton;
    }
    private void OnDestroy()
    {
        _OnPointerEnter.Clear();
    }
}
