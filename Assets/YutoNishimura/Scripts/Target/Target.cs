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
    private float initialTargetSpeed;       //�ړ����x
    private bool enableTakePicFlag;    //�^�[�Q�b�g�B�e����
    private const float disTargetShot = 7.0f;
    private bool destroyFlag;          //���g�i�^�[�Q�b�g�����ł����邩�ǂ�����\���t���O�j

    // Start is called before the first frame update
    void Start()
    {
        destroyFlag = false;
        enableTakePicFlag = false;
        agent = GetComponent<NavMeshAgent>();

        initialTargetSpeed = agent.speed;

        //�ڕW�n�_�̊Ԃ��p���I�Ɉړ�
        agent.autoBraking = false;
    }
    private void OnEnable()
    {
        //pointParent = GameObject.FindGameObjectWithTag("TargetPoint");
        //// �q�I�u�W�F�N�g�̐����擾
        //int childCount = pointParent.transform.childCount;

        //// �q�I�u�W�F�N�g�����Ɏ擾����
        //for (int i = 0; i < childCount; i++)
        //{
        //    Transform childTransform = pointParent.transform.GetChild(i);
        //    points[i] = childTransform.gameObject;
        //}
    }
    //private void OnEnable()
    //{
    //    targetCamera = GameObject.FindWithTag("subCamera");
    //    points = GameObject.FindGameObjectsWithTag("dest");
    //}

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isInsideCamera);
        //targetCamera.transform.position = new Vector3(
        //    this.transform.position.x,
        //    this.transform.position.y + 1.0f,
        //    this.transform.position.z);
        //targetCamera.transform.eulerAngles = this.transform.eulerAngles;
        //�G�[�W�F���g�����ڕW�n�_�ɋ߂Â����玟�̖ڕW�n�_��ݒ�

        //�܂��|�C���g���ݒ肳��Ă��Ȃ������珈���͍s��Ȃ�
        if(points == null)
        {
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoNextPoint();
        }

        if (destroyFlag)
        {
            Destroy(gameObject);
        }
    }

    //���g���J�����Ɏʂ��Ă����ꍇ�ɂ����Ăяo�����
    void OnWillRenderObject()
    {
        //���C���J�������猩�����Ƃ������������s��
        if (Camera.current.name == "Main Camera")
        {
            Debug.Log("���C���J�����������s���Ă��܂�");

            Vector3 strangeObjVec = transform.position - playerInstance.transform.position;
            Vector3 playerForwardVec = playerInstance.transform.forward;

            float angle = Vector3.Angle(playerForwardVec, strangeObjVec);

            float judgeDis = strangeObjVec.magnitude * Mathf.Cos((angle / 360) * Mathf.PI * 2);

            if (judgeDis <= disTargetShot)
            {
                enableTakePicFlag = true;
            }
            else
            {
                enableTakePicFlag = false;
            }
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

    public float GetTargetSpeed() { return agent.speed; }

    public float GetInitialTargetSpeed() { return initialTargetSpeed; }

    public void SetTargetSpeed(float speed) { agent.speed = speed; }

    public bool GetEnableTakePicFlag() { return enableTakePicFlag; }

    public void SetTargetRoot(List<GameObject> root) { points = root; }

    public void SetDestroyFlag(bool flag) { destroyFlag = flag; }
}
