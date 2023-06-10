using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : ButtonController
{
    [Header("������o�Ă���o�[")]
    [SerializeField] private Image bar;
    [Header("�o�[�̏����ʒu")]
    [SerializeField] private Image initialPos;
    [Header("�o�[�̈ړ���̈ʒu")]
    [SerializeField] private Image afterPos;

    
    


    private void Start()
    {
        //bar.rectTransform.position = bar.rectTransform.position;
        initialPos.rectTransform.position = bar.rectTransform.position;
        InitializeButton();
    }
    private void Update()
    {
        MoveScene(GameManager.Instance.sceneIndex);
    }
    public void PopUpForHome()
    {
        bar.rectTransform.DOMoveX(afterPos.rectTransform.position.x, Config.moveBarSpeed);
    }

    public void PopDownForHome()
    {
        bar.rectTransform.DOMoveX(initialPos.rectTransform.position.x, Config.moveBarSpeed);
    }
}
