using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseUIManager : MonoBehaviour
{
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
        _button = GetComponent<Button>();
        _image　= GetComponent<Image>();
        _text = transform.GetChild(0).GetComponent<Text>();
    }
    /// <summary>UI表示</summary>
    public void ShowUI()
    {
        //_button.enabled = true;
        _image.enabled = true;
        _text.enabled = true;
    }
    /// <summary>UI非表示</summary>
    public void HideUI()
    {
        //_button.enabled = false;
        _image.enabled = false;
        _text.enabled = false;
    }
}
