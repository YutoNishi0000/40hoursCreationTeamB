using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    /// <summary>�I�����{�^�� </summary>    
    private Button _button;

    /// <summary>�e�L�X�g </summary>    
    private Text _text;

    /// <summary>�{�^���N���X���擾 </summary>
    public Button Button => _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _text = transform.GetChild(0).GetComponent<Text>();
    }
}
