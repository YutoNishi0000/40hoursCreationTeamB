using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//���g�̎��ӂ�������悤�ɂ���
public class MetalonController : AIController
{
    private enum RootType
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh
    }

    private Vector3 initializeSpawnPos;
    private Animator animator;
    private int spawnNumber;    //�X�|�[���i���o�[
    private List<GameObject> root;
    private readonly float minDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        root = new List<GameObject>();
        //���g�̃X�|�[�����ꂽ�ʒu���L������
        initializeSpawnPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        destPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < minDistance)
        {
            GoNextPoint(root);
        }
        
        //��ɉ�������̃A�j���[�V�������Đ�����
        animator.SetFloat("Walk", agent.velocity.magnitude);
    }

    public override void GoNextPoint(List<GameObject> points)
    {
        base.GoNextPoint(points);
    }

    public override void SetPoints(GameObject prent, List<GameObject> points, int startNum)
    {
        base.SetPoints(prent, points, startNum);
    }

    //�I�[�o�[���[�h�֐�
    //���ς���K�v����i�֐�����SetPoints������悤�Ɂj
    public void SetRootType(int num, GameObject[] parentWonderPoints)
    {
        SetPoints(parentWonderPoints[(int)GetRootType(num)], root, GetStartPoint(num));
    }

    //�X�|�[���ԍ����烋�[�g�^�C�v���擾����֐�
    private RootType GetRootType(int num)
    {
        switch(num)
        {
            case 0:
                return RootType.First;
            case 3:
                return RootType.Second;
            case 7:
                return RootType.Third;
            case 9:
                return RootType.Fourth;
            case 5:
            case 6:
                return RootType.Fifth;
            case 2:
            case 8:
                return RootType.Sixth;
            default:
                return RootType.Seventh;
        }
    }

    //�ړI�n�̃��X�g�̉��Ԗڂ���X�^�[�g����΂悢�̂����L�q�����֐�
    private int GetStartPoint(int num)
    {
        switch(num)
        {
            case 1:
                return 2;
            case 5:
                return 1;
            case 10:
                return 5;
            default:
                return 0;
        }
    }
}
