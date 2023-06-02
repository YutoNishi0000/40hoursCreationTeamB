using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCount : MonoBehaviour
{
    // �\������UI
    [SerializeField] Image _speed;
    [SerializeField] Image _score;
    [SerializeField] Image _vision;
    // ��\������UI
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
        // �X�s�[�hUP
        _speed.enabled = !_skillManager.GetPlayerSpeedFlag();
        _speedOff.enabled = _skillManager.GetPlayerSpeedFlag();
        
        // �X�R�AUP
        _score.enabled = SkillManager.GetAddScoreFlag();
        _scoreOff.enabled = !SkillManager.GetAddScoreFlag();

        // �Ώۂ�5�b�ԉ���
        _vision.enabled = _skillManager.GetTargetMinimapFlag();
        _visionOff.enabled = !_skillManager.GetTargetMinimapFlag();
        
    }
}
