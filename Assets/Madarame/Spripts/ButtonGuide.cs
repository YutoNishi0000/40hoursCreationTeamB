using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGuide : MonoBehaviour
{
    /// <summary>テキスト </summary>     
    private Text _text;

    /// <summary>ボタンの説明[リスタート] </summary>  
    [Header("ボタンの説明[リスタート]"), SerializeField]
    private string _restartText;

    /// <summary>ボタンの説明[オプション] </summary>  
    [Header("ボタンの説明[オプション]"), SerializeField]
    private string _optionText;

    /// <summary>ボタンの説明[EXIT] </summary>  
    [Header("ボタンの説明[EXIT]"), SerializeField]
    private string _exitText;
    
    /// <summary>ボタンタイプ </summary>
    public enum ButtonType
    {
        restart, option, exit
    }
    private void Start()
    {
        _text = transform.GetChild(0).GetComponent<Text>();
    }
    /// <summary>ボタン説明を変える </summary>
    public void ChangeText(ButtonType type)
    {
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
                Debug.Log("説明用テキストがありません");
                break;
        }
    }
}
