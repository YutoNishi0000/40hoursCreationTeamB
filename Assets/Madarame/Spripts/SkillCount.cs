using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCount : MonoBehaviour
{
    // �\��/��\������UI
    [SerializeField] GameObject SkillCount0;
    [SerializeField] GameObject SkillCount1;
    [SerializeField] GameObject SkillCount2;
    // UI�؂�ւ��p�ϐ�
    int[] _SkillCount = new int[4];

    private void Update()
    {
        if (_SkillCount[1] == 1)
        {
            SkillCount0.gameObject.SetActive(true);
        }
        if (_SkillCount[2] == 2)
        {
            SkillCount1.gameObject.SetActive(true);
        }
        if (_SkillCount[3] == 3)
        {
            SkillCount2.gameObject.SetActive(true);
        }
    }
}
