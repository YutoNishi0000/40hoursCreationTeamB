using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeController : ButtonController
{
    [Header("横から出てくるバー")]
    [SerializeField] private Image bar;
    [Header("バーの初期位置")]
    [SerializeField] private Image initialPos;
    [Header("バーの移動後の位置")]
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
