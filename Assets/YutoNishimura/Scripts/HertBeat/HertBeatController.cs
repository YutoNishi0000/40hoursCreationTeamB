using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����̓������Ǘ�����N���X
//�v���C���[�ƃ^�[�Q�b�g�̋��������ȏ�k�܂����甏�����J�n����
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
