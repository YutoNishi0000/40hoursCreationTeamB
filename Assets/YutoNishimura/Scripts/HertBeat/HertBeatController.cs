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
        HeartBeatControl(playerInstance.gameObject.transform.position, playerInstance.targetCenter.transform.position);
    }

    public void HeartBeatControl(Vector3 playerPos, Vector3 targetPos)
    {
        if(Vector3.Distance(playerPos, targetPos) <= HERT_BEAT_DISTANCE)
        {
            GetComponent<HertBeatManager>().BeatUpdate();
            GetComponent<HertBeatManager>().FastBeat();
        }
    }
}
