using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//���g�̎��ӂ�������悤�ɂ���
public class MetalonController : Actor
{
    //���^�����i�����َ��Ȃ��́j�̃��[�g�̎��
    public enum SubRootType
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh
    }

    private Animator animator;
    public List<GameObject> points = new List<GameObject>();
    private int destPoint = 0;
    private NavMeshAgent agent;
    private List<GameObject> parentPoints;
    private readonly float minDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        parentPoints = new List<GameObject>();
        agent = GetComponent<NavMeshAgent>();
        //�ڕW�n�_�̊Ԃ��p���I�Ɉړ�
        agent.autoBraking = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < minDistance)
        {
            GoNextPoint();
        }

        animator.SetFloat("Run", agent.velocity.magnitude);
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

    //�^�[�Q�b�g�����ݏo���ꂽ����ɌĂяo��
    public void SetRootType(SubRootType type)
    {
        switch (type)
        {
            case SubRootType.First:
                SetPoints(parentPoints[(int)SubRootType.First]);
                break;
            case SubRootType.Second:
                SetPoints(parentPoints[(int)SubRootType.Second]);
                break;
            case SubRootType.Third:
                SetPoints(parentPoints[(int)SubRootType.Third]);
                break;
            case SubRootType.Fourth:
                SetPoints(parentPoints[(int)SubRootType.Fourth]);
                break;
            case SubRootType.Fifth:
                SetPoints(parentPoints[(int)SubRootType.Fifth]);
                break;
            case SubRootType.Sixth:
                SetPoints(parentPoints[(int)SubRootType.Sixth]);
                break;
            case SubRootType.Seventh:
                SetPoints(parentPoints[(int)SubRootType.Seventh]);
                break;
        }
    }

    //�X�|�[���ʒu�ɂ���ăZ�b�g���郋�[�g���ς��̂ŃX�|�[���ԍ��ɉ����ă��[�g�^�C�v���擾����֐�
    private SubRootType GetRootType(int num)
    {
        switch (num)
        {
            case 0:
                return SubRootType.First;
            case 3:
                return SubRootType.Second;
            case 7:
                return SubRootType.Third;
            case 9:
                return SubRootType.Fourth;
            case 5:
            case 6:
                return SubRootType.Fifth;
            case 2:
            case 8:
                return SubRootType.Sixth;
            default:
                return SubRootType.Seventh;
        }
    }

    //�ړI�n�̃��[�g���X�g�̉��Ԗڂ���X�^�[�g����΂悢�̂���Ԃ�
    public int GetStartPoint(int num)
    {
        switch (num)
        {
            case 0:
                return 7;
            case 1:
                return 5;
            case 4:
                return 13;
            case 5:
                return 3;
            case 6:
                return 7;
            case 7:
                return 5;
            case 8:
                return 7;
            case 9:
                return 8;
            case 10:
                return 10;
            default:
                return 0;
        }
    }

    public void SetSpawnNumber(int num) 
    {
        SetRootType(GetRootType(num));
        destPoint = GetStartPoint(num);
    }

    public void SetWonderParentPoints(List<GameObject> points) { parentPoints = points; }
}