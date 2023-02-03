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
