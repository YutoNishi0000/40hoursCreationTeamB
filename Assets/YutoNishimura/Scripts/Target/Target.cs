using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : Actor
{
    //public Transform[] points;
    public GameObject[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private GameObject targetCamera = null;
    private float initialTargetSpeed;       //�ړ����x

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        initialTargetSpeed = agent.speed;

        //�ڕW�n�_�̊Ԃ��p���I�Ɉړ�
        agent.autoBraking = false;
    }
    //private void OnEnable()
    //{
    //    targetCamera = GameObject.FindWithTag("subCamera");
    //    points = GameObject.FindGameObjectsWithTag("dest");
    //}

    // Update is called once per frame
    void Update()
    {
        //targetCamera.transform.position = new Vector3(
        //    this.transform.position.x,
        //    this.transform.position.y + 1.0f,
        //    this.transform.position.z);
        //targetCamera.transform.eulerAngles = this.transform.eulerAngles;
        //�G�[�W�F���g�����ڕW�n�_�ɋ߂Â����玟�̖ڕW�n�_��ݒ�
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoNextPoint();
        }
    }

    void GoNextPoint()
    {
        //�n�_�������ݒ肳��Ă��Ȃ��ꍇ
        if(points.Length == 0)
        {
            return;
        }

        //�G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ�
        agent.destination = new Vector3(points[destPoint].transform.position.x, transform.position.y, points[destPoint].transform.position.z);

        //�z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A�K�v�Ȃ�Ώo���n�_�ɖ߂�
        destPoint = (destPoint + 1) % points.Length;
    }

    public float GetTargetSpeed() { return agent.speed; }

    public float GetInitialTargetSpeed() { return initialTargetSpeed; }

    public void SetTargetSpeed(float speed) { agent.speed = speed; }
}
