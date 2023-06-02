using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCount : MonoBehaviour
{
    // 表示するUI
    [SerializeField] Image _speed;
    [SerializeField] Image _score;
    [SerializeField] Image _vision;
    // 非表示するUI
    [SerializeField] Image _speedOff;
    [SerializeField] Image _scoreOff;
    [SerializeField] Image _visionOff;

    SkillManager _skillManager;

    private void Start()
    {
        _skillManager = GetComponent<SkillManager>();
    }

    private void Update()
    {
        // スピードUP
        _speed.enabled = !_skillManager.GetPlayerSpeedFlag();
        _speedOff.enabled = _skillManager.GetPlayerSpeedFlag();
        
        // スコアUP
        _score.enabled = SkillManager.GetAddScoreFlag();
        _scoreOff.enabled = !SkillManager.GetAddScoreFlag();

        // 対象が5秒間可視化
        _vision.enabled = _skillManager.GetTargetMinimapFlag();
        _visionOff.enabled = !_skillManager.GetTargetMinimapFlag();
        
    }
}
