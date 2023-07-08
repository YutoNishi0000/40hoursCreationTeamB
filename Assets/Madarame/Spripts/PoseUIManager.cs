using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ButtonGuide;

public class PoseUIManager : MonoBehaviour
{
    /// <summary>ボタン説明 </summary>  
    [Header("ボタン説明"), SerializeField]
    private ButtonGuide _buttonGuide;

    /// <summary>ボタンの種類 </summary>  
    [Header("ボタンの種類"), SerializeField]
    private ButtonType _buttonType;

    /// <summary>オプションボタン </summary>  
    [Header("オプションボタン"), SerializeField]
    private HoverableButton _optionButton;

    /// <summary>選択肢ボタン </summary>    
    private Button _button;

    /// <summary>イメージ </summary>    
    private Image _image;

    /// <summary>テキスト </summary>    
    private Text _text = null;

    /// <summary>ボタンクラスを取得 </summary>
    public Button Button => _button;

    /// <summary>ボタンのテキスト変更</summary>
    private void ChangeText() => _buttonGuide.ChangeText(_buttonType);

    private void Start()
    {
        if(_buttonGuide == null || _optionButton == null)
        {
            Debug.LogError("インスペクターに格納してください。");
            return;
        }
        _button = GetComponent<Button>();
        _image　= GetComponent<Image>();

        _optionButton.CancelHover();
        
        _optionButton.AddOnPointerEnter(ChangeText);
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

}