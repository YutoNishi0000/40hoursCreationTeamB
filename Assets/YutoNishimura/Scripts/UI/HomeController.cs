using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeController : ButtonController
{
    [Header("������o�Ă���o�[")]
    [SerializeField] private Image bar;
    [Header("�o�[�̏����ʒu")]
    [SerializeField] private Image initialPos;
    [Header("�o�[�̈ړ���̈ʒu")]
    [SerializeField] private Image afterPos;

    private void Start()
    {
        bar.rectTransform.position = initialPos.rectTransform.position;
        tempButton = GetComponent<Image>();
    }

    public void PopUpForHome()
    {
        PopUp();
        bar.rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime);
    }

    public void PopDownForHome()
    {
        PopDown();
        bar.rectTransform.DOMoveX(initialPos.rectTransform.position.x, changeScaleTime);
    }
}
