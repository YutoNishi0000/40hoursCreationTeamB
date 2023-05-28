using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultController : UIController
{
    private enum TargetScore
    {
        Easy = 150,
        Nomal = 250,
        Hard = 500
    }

    [SerializeField] private List<Image> bars;
    [SerializeField] private Image afterPos;
    [SerializeField] private Image afterPos_Total;
    [SerializeField] private Image resultImg;
    [SerializeField] private float changeScaleTime = 0.3f;
    private Color initialResultImg;
    private float color_a;
    [SerializeField] private Text[] resultScores;
    [SerializeField] private Sprite clearSprite;       //ゲームクリア時に表示させるスプライト
    [SerializeField] private Sprite failedSprite;      //ゲーム失敗時に表示させるスプライト
    private const int titleIndex = 0;   //タイトルシーンのインデックス
    private void Start()
    {
        ShowResultScore();
        JudgeResult(GameManager.Instance.GetGameMode(), (int)ScoreManger.Score);
        initialResultImg = resultImg.color;
        resultImg.color = new Color(resultImg.color.r, resultImg.color.g, resultImg.color.b, 0);
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        ShowResult();

        if (GameManager.Instance.blockSwithScene)
        {
            return;
        }
        MoveScene(titleIndex);

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

    private void JudgeResult(GameManager.GameMode mode, int score)
    {
        switch(mode)
        {
            case GameManager.GameMode.Easy:
                resultImg.sprite = GetResultSprite(score, TargetScore.Easy);
                break;
            case GameManager.GameMode.Nomal:
                resultImg.sprite = GetResultSprite(score, TargetScore.Nomal);
                break;
            case GameManager.GameMode.Hard:
                resultImg.sprite = GetResultSprite(score, TargetScore.Hard);
                break;
        }
    }

    private Sprite GetResultSprite(int score, TargetScore targetScore)
    {
        if(score >= (int)targetScore)
        {
            return clearSprite;
        }
        else
        {
            return failedSprite;
        }
    }
}
