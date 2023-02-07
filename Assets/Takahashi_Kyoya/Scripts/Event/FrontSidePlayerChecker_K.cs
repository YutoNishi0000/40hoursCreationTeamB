using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontSidePlayerChecker_K : Human
{
    [SerializeField] private float RECOGNIZE_DISTANCE = 10;       //プレイヤーを認識できる距離
    [SerializeField] private Text timerText;
    [SerializeField] private float time;
    [SerializeField] private float RECOGNIZE_PLAYER_TIME = 3f;          //ターゲットがプレイヤーを認識するまでの時間
    private TargetController targetController;
    private bool _isEscape;                                       //今逃走中かどうか
    private bool _isRecognizeBack;                                    //プレイヤーを認識しているかどうか(後方範囲)

    private void Start()
    {
        _isEscape = false;
        timerText.enabled = false;
        time = 0;
        targetController = GetComponentInParent<TargetController>();
    }

    private void Update()
    {
        if (/*_frontChecker.CheckPlayerFront() || */_isRecognizeBack)
        {
            GameManager.Instance.SetInContactArea(true);
            CountTimer();
        }
        else
        {
            GameManager.Instance.SetInContactArea(false);
            OffTimer();
        }
    }

    //前方を
    public bool CheckPlayerFront()
    {
        if (_isRecognizeBack)
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
            Negotiation();
        }
    }

    public void OffTimer()
    {
        time = 0;
        timerText.enabled = false;
    }

    void Negotiation()
    {
        //====================================================================
        //====================================================================
        //
        // ターゲットが逃走したときの処理
        //
        //====================================================================
        //====================================================================

        Debug.Log("交渉イベント！");
        Day5.day5 = true;
    }

    void SetTargetStateEscape()
    {
        _isEscape = true;
        targetController.SettargetState(TargetController.TargetState.Escape);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //レイをプレイヤーの方向に飛ばして何も当たらなかったら
            if (Physics.Raycast(transform.position, other.gameObject.transform.position - transform.position, 10f, LayerMask.GetMask("Player")))
            {
                _isRecognizeBack = true;
            }
            else
            {
                _isRecognizeBack = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isRecognizeBack = false;
        }
    }
}
