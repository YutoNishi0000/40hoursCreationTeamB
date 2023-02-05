using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontSidePlayerChecker : Human
{
    [SerializeField] private float RECOGNIZE_DISTANCE = 10;       //プレイヤーを認識できる距離
    [SerializeField] private Text timerText;
    [SerializeField] private float time;
    [SerializeField] private float RECOGNIZE_PLAYER_TIME = 3f;          //ターゲットがプレイヤーを認識するまでの時間

    private void Start()
    {
        timerText.enabled = false;
        time = 0;
    }

    //前方を
    public bool CheckPlayerFront()
    {
        RaycastHit hit;
        if (Physics.BoxCast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Vector3(2, 2, 4), transform.forward, out hit, Quaternion.identity, 10f, LayerMask.GetMask("Player")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CountTimer()
    {
        timerText.enabled = true;
        time += Time.deltaTime;
        timerText.text = Mathf.FloorToInt(time) + "秒";

        //ターゲットがプレイヤーを認識した時の処理
        if(time >= RECOGNIZE_PLAYER_TIME)
        {
            GetAway();
        }
    }

    public void OffTimer()
    {
        time = 0;
        timerText.enabled = false;
    }

    void GetAway()
    {
        //====================================================================
        //====================================================================
        //
        // ターゲットが逃走したときの処理
        //
        //====================================================================
        //====================================================================

        Debug.Log("逃走イベント発生！");
    }
}
