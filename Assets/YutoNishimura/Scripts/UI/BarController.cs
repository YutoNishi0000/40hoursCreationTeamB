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

    private const int sceneIndex = 2;

    private void Start()
    {
        //bar.rectTransform.position = bar.rectTransform.position;
        initialPos.rectTransform.position = bar.rectTransform.position;
        InitializeButton();
    }
    private void Update()
    {
        if (GameManager.Instance.blockSwithScene)
        {
            return;
        }
        MoveScene(sceneIndex);
    }
    public void PopUpForHome()
    {
        bar.rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime);
        SEManager.Instance.PlaySelect();
    }

    public void PopDownForHome()
    {
        bar.rectTransform.DOMoveX(initialPos.rectTransform.position.x, changeScaleTime);
    }
    public void Instnt()
    {
        Instantiate(endAnimation);
    }
}
