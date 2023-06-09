using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    /// <summary>選択肢ボタン </summary>    
    private Button _button;

    /// <summary>テキスト </summary>    
    private Text _text;

    /// <summary>ボタンクラスを取得 </summary>
    public Button Button => _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _text = transform.GetChild(0).GetComponent<Text>();
    }
}
