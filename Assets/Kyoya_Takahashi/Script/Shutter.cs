using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    //�B�e�̃N�[���^�C��
    [SerializeField] private float coolTime = 3f;
    //�B�e�\��
    private bool isEnable = true;
    //�B�e����
    public static bool isFilming = false;
    void Update()
    {
        isFilming = false;
        if (Input.GetMouseButtonDown(0))
        {
            // �R���[�`�����J�n����
            StartCoroutine(StartTimer());
            isFilming = true;
        }
    }
    /// <summary>
    /// �J�����̃N�[���^�C��
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartTimer()
    {
        // �J�����g�p�s��
        isEnable = false;
        // coolTime(3)�b�҂�
        yield return new WaitForSeconds(coolTime);
        // coolTime(3)�b��J�����g�p�\
        isEnable = true;
    }
}
