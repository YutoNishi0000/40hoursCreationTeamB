using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーが察知範囲内に居たらゲージを減らして行く
public class PoliceController : MonoBehaviour
{
    private bool _policeCheckupFlag;            //職質危険察知フラグ
    public Slider _dangerDetectionGauge;        //危険度察知ゲージ

    // Start is called before the first frame update
    void Start()
    {
        _policeCheckupFlag = false;

        if(_dangerDetectionGauge == null)
        {
            Debug.LogError("危険度察知ゲージを入れてください");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _policeCheckupFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _policeCheckupFlag = false;
        }
    }
}
