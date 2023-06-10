using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseUIManager : MonoBehaviour
{
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
        _button = GetComponent<Button>();
        _image�@= GetComponent<Image>();
        _text = transform.GetChild(0).GetComponent<Text>();
    }
    /// <summary>UI�\��</summary>
    public void ShowUI()
    {
        //_button.enabled = true;
        _image.enabled = true;
        _text.enabled = true;
    }
    /// <summary>UI��\��</summary>
    public void HideUI()
    {
        //_button.enabled = false;
        _image.enabled = false;
        _text.enabled = false;
    }
}
