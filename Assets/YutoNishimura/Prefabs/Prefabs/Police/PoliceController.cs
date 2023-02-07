using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーが察知範囲内に居たらゲージを減らして行く
public class PoliceController : MonoBehaviour
{
    private static bool _policeCheckupFlag;            //職質危険察知フラグ
    public Slider[] _dangerDetectionGauge;        //危険度察知ゲージ
    private readonly float CAUGHT_TIME = 4;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        for(int i = 0; i < _dangerDetectionGauge.Length; i++)
        {
            _dangerDetectionGauge[i].maxValue = CAUGHT_TIME;
        }

        _policeCheckupFlag = false;

        if(_dangerDetectionGauge == null)
        {
            Debug.LogError("危険度察知ゲージを入れてください");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //職質危険察知フラグがオンだった場合は
        if (_policeCheckupFlag)
        {
            Debug.Log("プレイヤーを職質しようかな・・・");
            for (int i = 0; i < _dangerDetectionGauge.Length; i++)
            {
                _dangerDetectionGauge[i].value -= Time.deltaTime;

                //もしもスライダーの値が０より小さくなったら
                if(_dangerDetectionGauge[i].value <= 0)
                {
                    Debug.Log("職質開始！！");
                    PoliceEvent();
                }
            }
        }
        //職質危険察知フラグがオフだった場合は
        else
        {
            //ゲージの値をもとに戻す
            for (int i = 0; i < _dangerDetectionGauge.Length; i++)
            {
                _dangerDetectionGauge[i].DOValue(_dangerDetectionGauge[i].maxValue, CAUGHT_TIME / 3);
            }
        }
    }

    /// <summary>
    /// 警官に職質された時のイベント
    /// </summary>
    void PoliceEvent()
    {
        //====================================================================================
        //====================================================================================
        //
        // ここに警官から職質された際の処理を記述してください
        //
        //====================================================================================
        //====================================================================================
    }

    private void OnTriggerEnter(Collider other)
    {
        //もしもプレイヤーが察知範囲内に進入してきたら
        if(other.gameObject.CompareTag("Player"))
        {
            //フラグをオンにする
            _policeCheckupFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //もしもプレイヤーが察知範囲内に進入してきたら
        if (other.gameObject.CompareTag("Player"))
        {
            //フラグをオフにする
            _policeCheckupFlag = false;
        }
    }
}
