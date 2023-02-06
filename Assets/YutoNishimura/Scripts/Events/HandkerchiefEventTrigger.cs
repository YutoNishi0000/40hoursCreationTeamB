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
    /// ハンカチを拾ったときに処理する関数
    /// </summary>
    void HandkerchiefEvent()
    {
        //======================================================================
        //======================================================================
        //
        // ここにハンカチを拾ったときの処理を記述してください
        //
        //======================================================================
        //======================================================================

        Debug.Log("ハンカチを拾った時のイベント発生");

        UIController._talkStart = true;
    }
}
