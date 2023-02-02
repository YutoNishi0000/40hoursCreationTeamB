using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMouseInputController : MonoBehaviour
{
    private Vector3 _currentmousePos;      //���݂̃}�E�X�ʒu
    private Vector3 _previousemousePos;    //1�t���[���O�̃}�E�X�̈ʒu
    private float _decisionRight;          //���背�[���̉E����x�l
    private float _decisionLeft;           //���背�[���̍�����x�l
    private readonly float LANE_WIDTH = 200;
    private bool _moiseAction;

    // Start is called before the first frame update
    void Start()
    {
        //�X�N���[���̕����i�[
        _decisionRight = Screen.width;
        _decisionLeft = Screen.width - LANE_WIDTH;
    }

    private void FixedUpdate()
    {
        //���݂̃}�E�X�̈ʒu���擾
        _currentmousePos = Input.mousePosition;

        //�����ŉ�������̏�����������
        //������1�t���[���O�̃}�E�X�̈ʒu�ƌ��݂̃}�E�X�̈ʒu������Ă��āA���݂̃}�E�X�̈ʒu�����[�����ɂ���̂ł����
        if(_currentmousePos != _previousemousePos && _decisionLeft <= _currentmousePos.x && _currentmousePos.x <= _decisionRight)
        {
            Debug.Log("�}�E�X�̈ʒu���ς����");
            _moiseAction = true;

            //==============================================================
            // �����Ŕ�������
            //==============================================================
        }
        else
        {
            _moiseAction = false;
        }

        _previousemousePos = _currentmousePos;
    }

    public bool GetMouseAction()
    {
        return _moiseAction;
    }
}
