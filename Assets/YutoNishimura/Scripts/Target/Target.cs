using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : Actor
{
    //public Transform[] points;
    public List<GameObject> points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private GameObject targetCamera = null;
    private float initialTargetSpeed;       //�ړ����x
    private GameObject pointParent = null;
    private GameObject rootParent1;
    private GameObject rootParent2;
    private GameObject rootParent3;
    private GameObject rootParent4;
    private bool isInsideCamera = false;
    private bool enableTakePicFlag;    //�^�[�Q�b�g�B�e����
    private const float disTargetShot = 7.0f;
    private readonly float minDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        enableTakePicFlag = false;
        agent = GetComponent<NavMeshAgent>();

        //int rootType = Random.Range(0, 4);
        //SetRootType((RespawTarget.RootType)rootType);

        initialTargetSpeed = agent.speed;

        //�ڕW�n�_�̊Ԃ��p���I�Ɉړ�
        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < minDistance)   
        {
            GoNextPoint();
        }
    }

    void GoNextPoint()
    {
        //�n�_�������ݒ肳��Ă��Ȃ��ꍇ
        if (points.Count == 0)
        {
            return;
        }

        //�G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ�
        agent.destination = new Vector3(points[destPoint].transform.position.x, transform.position.y, points[destPoint].transform.position.z);

        //�z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A�K�v�Ȃ�Ώo���n�_�ɖ߂�
        destPoint = (destPoint + 1) % points.Count;
    }

    public void SetPoints(GameObject prent)
    {
        // �q�I�u�W�F�N�g�̐����擾
        int childCount = prent.transform.childCount;

        // �q�I�u�W�F�N�g�����Ɏ擾����
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = prent.transform.GetChild(i);
            points.Add(childTransform.gameObject);
        }
    }

    public float GetTargetSpeed() { return agent.speed; }

    public float GetInitialTargetSpeed() { return initialTargetSpeed; }

    public void SetTargetSpeed(float speed) { agent.speed = speed; }

    public bool GetEnableTakePicFlag() { return enableTakePicFlag; }

    //�^�[�Q�b�g�����ݏo���ꂽ����ɌĂяo���i�w�肳�ꂽ���[�g�^�C�v�ɂ���ăZ�b�g���郋�[�g���قȂ�j
    public void SetRootType(RespawTarget.RootType type)
    {
        switch(type)
        {
            case RespawTarget.RootType.First:
                rootParent1 = GameObject.Find("Root1");
                SetPoints(rootParent1);
                break;
            case RespawTarget.RootType.Second:
                rootParent2 = GameObject.Find("Root2");
                SetPoints(rootParent2);
                break;
            case RespawTarget.RootType.Third:
                rootParent3 = GameObject.Find("Root3");
                SetPoints(rootParent3);
                break;
            case RespawTarget.RootType.Fourth:
                rootParent4 = GameObject.Find("Root4");
                SetPoints(rootParent4);
                break;
        }
    }
}
