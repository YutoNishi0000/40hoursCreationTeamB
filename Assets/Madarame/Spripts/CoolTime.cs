using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTime : MonoBehaviour
{
    // �J�����̃N�[���^�C��
    [SerializeField] float coolTime = 3.0f;
    // �J����������ł��邩/�ł��Ȃ���
    public bool IsEnable { get; private set; } = true;

    private IEnumerator StartTimer()
    {
        // �J�����g�p�s��
        IsEnable = false;
        // coolTime(3)�b�҂�
        yield return new WaitForSeconds(coolTime);
        // coolTime(3)�b��J�����g�p�\
        IsEnable = true;
    }
    public void StartCoolTime()
    {
        if(!IsEnable)
        {
            return;
        }
        // �R���[�`�����J�n����
        StartCoroutine(StartTimer());
    }
}
