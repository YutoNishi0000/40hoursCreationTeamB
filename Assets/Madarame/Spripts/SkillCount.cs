using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCount : MonoBehaviour
{
    // 表示するUI
    [SerializeField] GameObject _speed;
    [SerializeField] GameObject _score;
    [SerializeField] GameObject _vision;
    // 非表示するUI
    [SerializeField] GameObject _speedOff;
    [SerializeField] GameObject _scoreOff;
    [SerializeField] GameObject _visionOff;
    // UI切り替え用変数
    int[] _Skill = new int[4];

    private void Update()
    {
        // スピードUP
        if (_Skill[1] == 1)
        {
            _speed.gameObject.GetComponent<Image>().enabled = true;
            _speedOff.gameObject.GetComponent<Image>().enabled = false;
        }
        // スコアUP
        if (_Skill[2] == 2)
        {
            _score.gameObject.GetComponent<Image>().enabled = true;
            _scoreOff.gameObject.GetComponent<Image>().enabled = false;
        }
        // 対象が5秒間可視化
        if (_Skill[3] == 3)
        {
            Invoke("Vision", 5);
            _vision.gameObject.GetComponent<Image>().enabled = false;
            _visionOff.gameObject.GetComponent<Image>().enabled = true;
        }
    }
    private void Vision()
    {
        _vision.gameObject.GetComponent<Image>().enabled = true;
        _visionOff.gameObject.GetComponent<Image>().enabled = false;
    }
}
