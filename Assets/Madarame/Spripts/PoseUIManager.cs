using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ButtonGuide;

public class PoseUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>ボタン説明 </summary>  
    [Header("ボタン説明"), SerializeField]
    private ButtonGuide _buttonGuide;

    /// <summary>ボタンの種類 </summary>  
    [Header("ボタンの種類"), SerializeField]
    private ButtonType _buttonType;

    /// <summary>選択されている時のボタン画像 </summary>  
    [Header("選択されている時のボタン画像"), SerializeField]
    private Sprite _selectButton;

    /// <summary>選択されていない時のボタン画像 </summary>  
    [Header("選択されていない時のボタン画像"), SerializeField]
    private Sprite _noselectButton;

    /// <summary>選択肢ボタン </summary>    
    private Button _button;

    /// <summary>イメージ </summary>    
    private Image _image;

    /// <summary>テキスト </summary>    
    private Text _text;

    /// <summary>ボタンクラスを取得 </summary>
    public Button Button => _button;
    
    private void Start()
    {
        if(_buttonGuide == null || _selectButton == null || _noselectButton == null)
        {
            Debug.LogError("インスペクターに格納してください。");
            return;
        }
        _button = GetComponent<Button>();
        _image　= GetComponent<Image>();
        _button.image.sprite = _noselectButton;

        // 選択されていないときはボタンを暗くする
        //Color disabledColor = Button.colors.disabledColor;
        //Button.image.color = disabledColor;
    }
    /// <summary>UI表示</summary>
    public void ShowUI()
    {
        _image.enabled = true;
        _text.enabled = true;
    }
    /// <summary>UI非表示</summary>
    public void HideUI()
    {
        _image.enabled = false;
        _text.enabled = false;
    }
    /// <summary>選択されてるときはボタンを明るくする</summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonGuide.ChangeText(_buttonType);
        _button.image.sprite = _selectButton;
    }
    /// <summary>選択されていないときはボタンを暗くする</summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        _button.image.sprite = _noselectButton;
    }
}
