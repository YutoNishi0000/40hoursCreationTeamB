using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandkerchiefEventController : MonoBehaviour
{
    private FrontSidePlayerChecker _frontChecker;

    Message message = null;
    GameObject messageUI = null;

    // Start is called before the first frame update
    void Start()
    {
        _frontChecker = GetComponent<FrontSidePlayerChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_frontChecker.CheckPlayerFront())
        {
            _frontChecker.CountTimer();
        }
        else
        {
            _frontChecker.OffTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            HankatiEvent();
        }
    }

    void HankatiEvent()
    {
        //=================================================================================================
        //=================================================================================================
        //
        // ここにハンカチイベントの会話シーンなどの処理を記述してください
        //
        //=================================================================================================
        //=================================================================================================
        Debug.Log("ハンカチイベント発生");
        
        //GameManager.Instance.SetInContactArea(true);
        //Debug.Log(GameManager.Instance.GetInContactArea());
    }
}
