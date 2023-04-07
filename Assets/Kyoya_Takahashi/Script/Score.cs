using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //�e�L�X�g�I�u�W�F�N�g
    Text currentScoreText;
    //���݂̃X�R�A
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
