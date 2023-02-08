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
    private TargetController targetController;
    private bool _isEscape;                                       //今逃走中かどうか
    private bool _isRecognizeBack;                                    //プレイヤーを認識しているかどうか(後方範囲)
    private bool _isEvent;                                      //今イベント中かどうか
    //public static bool _isEscape;
    public static bool _Escaped;                                        //逃走されたかどうか


    private void Start()
    {
        _isEscape = false;
        _isEvent = false;
        _isEscape = false;
        timerText.enabled = false;
        time = 0;
        targetController = GetComponentInParent<TargetController>();
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

        if (!_isEscape)
        {
            targetController.SettargetState(TargetController.TargetState.LookPlayer);
        }

        if(_isEvent)
        {
            return;
        }

        Invoke(nameof(SetTargetStateEscape), 2);

        Debug.Log("逃走イベント発生！");
    }

    void SetTargetStateEscape()
    {
        _isEscape = true;
        targetController.SettargetState(TargetController.TargetState.Escape);
        Invoke(nameof(GameOver), 1);
    }

    void GameOver()
    {
        _Escaped = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイy－検知");
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

    public void TriggerEvent()
    {
        _isEvent = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isRecognizeBack = false;
        }
    }
}
