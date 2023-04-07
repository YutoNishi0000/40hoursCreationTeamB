using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //テキストオブジェクト
    Text currentScoreText;
    //現在のスコア
    private int currntScore = 0;
    private void Awake()
    {
        currentScoreText = this.GetComponent<Text>();
    }
    private void LateUpdate()
    {
        currntScore += CameraScore.Score;
        CameraScore.Score = 0;
        GameManager.Instance.IsPhoto = false;
        Debug.Log(currntScore.ToString());
        TextUpdate();
    }
    void TextUpdate()
    {
        currentScoreText.text = currntScore.ToString();
    }
}
