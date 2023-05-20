using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultController : UIController
{
    [SerializeField] private List<Image> bars;
    [SerializeField] private Image afterPos;
    [SerializeField] private Image resultImg;
    [SerializeField] private float changeScaleTime = 0.3f;
    private Color initialResultImg;
    private float color_a;

    private void Start()
    {
        initialResultImg = resultImg.color;
        resultImg.color = new Color(resultImg.color.r, resultImg.color.g, resultImg.color.b, 0);
    }

    private void Update()
    {
        ShowResult();
    }

    void ShowResult()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(bars[0].rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime))
                .Append(bars[1].rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime))
                .Append(bars[2].rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime))
                .Append(bars[3].rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime))
                .Append(bars[4].rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime))
                .Append(bars[5].rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime))
                .Append(resultImg.DOColor(initialResultImg, changeScaleTime));
    }
}
