using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Handkerchief"))
        {
            HandkerchiefEvent();
        }
    }

    /// <summary>
    /// �n���J�`���E�����Ƃ��ɏ�������֐�
    /// </summary>
    void HandkerchiefEvent()
    {
        //======================================================================
        //======================================================================
        //
        // �����Ƀn���J�`���E�����Ƃ��̏������L�q���Ă�������
        //
        //======================================================================
        //======================================================================

        Debug.Log("�n���J�`���E�������̃C�x���g����");

        UIController._talkStart = true;
    }
}
