using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TargetController : Human
{
    private NavMeshAgent _navmeshAgent;    //�i�r���b�V���G�[�W�F���g
    private Animator _animator;
    public GameObject[] passingPoints;     //�s���̒ʉ߃|�C���g
    private bool _passAllPoints;           //�S�Ă̒ʉ݃|�C���g��ʉ߂������ǂ���
    private int _pointIndex;
    private float _lockedOnTime;
    [SerializeField] private float RECOGNIZE_TIME = 3;
    private bool _moveLock;               //�ړ��𐧌����邽�߂̃t���O

    //=====================================================================
    // �ʉ߃|�C���g�Ɋւ���
    //=====================================================================
    //
    //�E�������������Ԃ�Gameobject�z���GameObject�����ĉ�����
    //
    //=====================================================================

    // Start is called before the first frame update
    void Start()
    {
        _moveLock = false;
        _lockedOnTime = 0;
        _pointIndex = 0;
        _passAllPoints = false;
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        //�z��̈�ԍŏ��̗v�f���ړI�n�ɂ�������Ή��̏����̃R�����g�A�E�g���O���Ă�����s���̏������R�����g�A�E�g���Ă�������
        //_navmeshAgent.SetDestination(passingPoints[_pointIndex].transform.position);
        transform.position = passingPoints[_pointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();

        CheckPlayer();
    }

    public override void MoveControl()
    {
        if (_moveLock)
        {
            StopMove();
            return;
        }

        //�A�j���[�V�����֘A����
        AnimationControl(_navmeshAgent.velocity.magnitude);

        //�o�H�������Ă���Ȃ珈�����Ȃ�
        if (_navmeshAgent.hasPath)
        {
            return;
        }

        //���݃Z�b�g����Ă���ړI�n�̔z��ԍ����z��̒�������P����������菬��������_passAllPoints��false��������
        if (_pointIndex < passingPoints.Length - 1 && !_passAllPoints)
        {
            //�z��ɂ��鎟�̗v�f���i�[
            _navmeshAgent.SetDestination(passingPoints[_pointIndex++].transform.position);
        }
        else
        {
            //�����ɓ����Ă����Ƃ������Ƃ͑S�Ẵ|�C���g������ĂƂ������ƂȂ̂�_passAllPoints��true�ɂ���
            _passAllPoints = true;

            //�ړI�n�͔z��̈�O�̗v�f���w�肵�ė�������߂�悤�ɂ���
            _navmeshAgent.SetDestination(passingPoints[_pointIndex--].transform.position);

            //�������Č������ʒu�ɖ߂��Ă�����
            if (_pointIndex == 0)
            {
                //_passAllPoints��false�ɂ��Ă��̕��򂩂甲����
                _passAllPoints = false;
            }
        }
    }

    public void AnimationControl(float velocity)
    {
        //�����Ƃ��ēn���ꂽ�l��0���傫�����
        if (velocity > .1)
        {
            //�A�j���[�V�������Đ�����
            _animator.SetFloat("Speed", 1);
        }
        //�����Ƃ��ēn���ꂽ�l��0�ȉ��ł����
        else
        {
            //�A�j���[�V�������~�߂�
            _animator.SetFloat("Speed", 0);
        }
    }

    /// <summary>
    /// �v���C���[�Ɏ��_�����b�N����Ă�����J�E���g���J�n���A�v���C���[�̕����������悤�ɂ���
    /// </summary>
    void CheckPlayer()
    {
        if (RaycastController.Lockon)
        {
            _lockedOnTime += Time.deltaTime;

            if (_lockedOnTime > RECOGNIZE_TIME)
            {
                //�ړ����b�N�I��
                _moveLock = true;

                //y�������v���C���[�̕���������������
                Vector3 target = playerInstance.gameObject.transform.position;
                target.y = this.transform.position.y;
                this.transform.LookAt(target);
                Debug.Log("�v���C���[�ɋC�Â���");
            }
        }
    }

    /// <summary>
    /// �ړ��A�A�j���[�V�����S�Ă̏������~�߂�
    /// </summary>
    void StopMove()
    {
        _navmeshAgent.isStopped = true;     //�ړ�����߂�����
        AnimationControl(0);                //�A�j���[�V�����Đ����~�߂�
    }
}
