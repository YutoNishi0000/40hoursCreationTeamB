using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//拍動の動きを管理するクラス
//プレイヤーとターゲットの距離が一定以上縮まったら拍動を開始する
public class HertBeatController : Human
{
    [SerializeField] private float HERT_BEAT_DISTANCE = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HeartBeatControl();
    }

    public void HeartBeatControl()
    {
        //if(RaycastController.BeatHeart)
        //{
        //    GetComponent<HertBeatManager>().BeatUpdate();
        //    GetComponent<HertBeatManager>().FastBeat();
        //}
    }
}
