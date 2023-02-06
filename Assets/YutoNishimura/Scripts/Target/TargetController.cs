using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TargetController : Human
{
    public enum TargetState
    {
        Walk,
        LookPlayer,
        Escape
    }

    private enum RouteDirection
    {
        Forward,
        Back
    }

    private NavMeshAgent _navmeshAgent;    //�i�r���b�V���G�[�W�F���g
    private Animator _animator;
    public GameObject[] passingPoints;     //�s���̒ʉ߃|�C���g
    private bool _passAllPoints;           //�S�Ă̒ʉ݃|�C���g��ʉ߂������ǂ���
    private int _pointIndex;
    private float _lockedOnTime;
    [SerializeField] private float RECOGNIZE_TIME = 3;
    private bool _moveLock;               //�ړ��𐧌����邽�߂̃t���O
    private TargetState _targetState;
    private bool _escape;                    //�������Ă邩�ǂ���
    [SerializeField] private readonly float ESCAPE_SPEED = 10;
    RouteDirection routDir;

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
        _escape = false;
        _moveLock = false;
        _lockedOnTime = 0;
        _pointIndex = 0;
        _passAllPoints = false;
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _targetState = new TargetState();
        //�z��̈�ԍŏ��̗v�f���ړI�n�ɂ�������Ή��̏����̃R�����g�A�E�g���O���Ă�����s���̏������R�����g�A�E�g���Ă�������
        //_navmeshAgent.SetDestination(passingPoints[_pointIndex].transform.position);
        transform.position = passingPoints[_pointIndex].transform.position;
        routDir = new RouteDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("���̃|�C���g�C���f�b�N�X��" + _pointIndex);

        switch(_targetState)
        {
            case TargetState.Walk:
                CheckPlayer();
                MoveControl();
                break;

            case TargetState.LookPlayer:
                StopMove();
                LookPlayer();
                break;

            case TargetState.Escape:
                UnLockMove();
                MoveControl();
                Debug.Log(_pointIndex);
                break;
        }
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
            //Debug.Log("���[�g����������܂�");
            //�z��ɂ��鎟�̗v�f���i�[
            routDir = RouteDirection.Forward;
            _pointIndex++;
            _navmeshAgent.SetDestination(passingPoints[_pointIndex].transform.position);

            //Debug.Log("���[�g����������̃C���f�b�N�X��" + _pointIndex);
        }
        else
        {
            routDir = RouteDirection.Back;
            //Debug.Log("�����烋�[�g���]���܂�");

            //�����ɓ����Ă����Ƃ������Ƃ͑S�Ẵ|�C���g������ĂƂ������ƂȂ̂�_passAllPoints��true�ɂ���
            _passAllPoints = true;

            //�ړI�n�͔z��̈�O�̗v�f���w�肵�ė�������߂�悤�ɂ���
            _pointIndex--;
            _navmeshAgent.SetDestination(passingPoints[_pointIndex].transform.position);

            Debug.Log("���̃C���f�b�N�X��" + _pointIndex);

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
                LookPlayer();
                //Debug.Log("�v���C���[�ɋC�Â���");
            }
        }
    }

    /// <summary>
    /// �v���C���[�̕������������߂̊֐�
    /// </summary>
    void LookPlayer()
    {
        //y�������v���C���[�̕���������������
        Vector3 target = playerInstance.gameObject.transform.position;
        target.y = this.transform.position.y;
        this.transform.LookAt(target);
    }

    /// <summary>
    /// �ړ��A�A�j���[�V�����S�Ă̏������~�߂�
    /// </summary>
    void StopMove()
    {
        _navmeshAgent.isStopped = true;     //�ړ�����߂�����
        AnimationControl(0);                //�A�j���[�V�����Đ����~�߂�

        if(!_escape)
        {
            switch(routDir)
            {
                case RouteDirection.Forward:
                    _passAllPoints = true;
                    _pointIndex--;
                    break;

                case RouteDirection.Back:
                    _passAllPoints = false;
                    _pointIndex++;
                    break;
            }

            _navmeshAgent.SetDestination(passingPoints[_pointIndex].transform.position);
            _escape = true;
        }
    }

    /// <summary>
    /// ���[�u���b�N���ꂽ������Ăыt���[�g�ɓ����n�߂邽�߂̊֐�
    /// </summary>
    void UnLockMove()
    {
        _moveLock = false;
        _navmeshAgent.isStopped = false;
        _navmeshAgent.speed = ESCAPE_SPEED;
    }

    /// <summary>
    /// 
    /// </summary>
    void EscapeFromPlayer()
    {

    }

    /// <summary>
    /// �^�[�Q�b�g�X�e�[�g�̃Z�b�^�[
    /// </summary>
    /// <param name="targetState"></param>
    public void SettargetState(TargetState targetState)
    {
        _targetState = targetState;
    }

    /// <summary>
    /// �^�[�Q�b�g�X�e�[�g�̃Q�b�^�[
    /// </summary>
    /// <returns></returns>
    public TargetState GetTargerState()
    {
        return _targetState;
    }
}
