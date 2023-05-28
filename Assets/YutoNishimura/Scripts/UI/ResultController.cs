using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultController : UIController
{
    [SerializeField] private List<Image> bars;
    [SerializeField] private Image afterPos;
    [SerializeField] private Image afterPos_Total;
    [SerializeField] private Image resultImg;
    [SerializeField] private float changeScaleTime = 0.3f;
    private Color initialResultImg;
    private float color_a;
    [SerializeField] private Text[] resultScores;

    private void Start()
    {
        ShowResultScore();
        initialResultImg = resultImg.color;
        resultImg.color = new Color(resultImg.color.r, resultImg.color.g, resultImg.color.b, 0);
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        ShowResult();
    }

    public void ShowResultScore()
    {
        resultScores[0].text = GameManager.Instance.numSubShutter.ToString();
        resultScores[1].text = GameManager.Instance.numLowScore.ToString();
        resultScores[2].text = GameManager.Instance.numMiddleScore.ToString();
        resultScores[3].text = GameManager.Instance.numHighScore.ToString();
        resultScores[4].text = ScoreManger.Score.ToString();
    }

    void ShowResult()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(bars[0].rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime))
                .Append(bars[1].rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime))
                .Append(bars[2].rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime))
                .Append(bars[3].rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime))
                .Append(bars[4].rectTransform.DOMoveX(afterPos_Total.rectTransform.position.x, changeScaleTime))
                .Append(resultImg.DOColor(initialResultImg, changeScaleTime));
    }
}
