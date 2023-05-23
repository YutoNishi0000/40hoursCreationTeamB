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
        if(!isEnable)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("���N���b�N���ꂽ");
            SEManager.Instance.PlayShot();
            isFilming = true;
        }
        else
        {
            return;
        }
        // �R���[�`�����J�n����
        StartCoroutine(StartTimer());
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
