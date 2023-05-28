using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text text = null;
    private void Awake()
    {
        text = this.GetComponent<Text>();
    }
    private void Update()
    {
        text.text = ScoreManger.Score.ToString();
    }
}
