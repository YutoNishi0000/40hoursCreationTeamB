using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�ړ����x
    [SerializeField] float walkSpeed = 5.0f;
    [SerializeField] float runSpeed = 10.0f;
    //�����L�[���͒l
    private float inputH = 0;
    private float inputV = 0;
    //���x
    private Vector3 velocity = Vector3.zero;
    private Vector3 oldVelocity = Vector3.zero;
    //���݂̈ړ����x
    private float currentMoveSpeed = 0;
    //�R���|�[�l���g
    private Rigidbody _rigidbody = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        InputUpdate();
        MoveUpdate();
    }
    private void FixedUpdate()
    {
        MoveFixedUpdate();
    }
    /// <summary>
    /// �L�[���͂̎擾
    /// </summary>
    private void InputUpdate()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");
    }
    /// <summary>
    /// �ړ��v�Z
    /// </summary>
    private void MoveUpdate()
    {
        float lerpSpeed = 5.0f;
        currentMoveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        //�ړ������ƈړ��ʂ̎Z�o
        velocity = ((transform.forward * inputV) + (transform.right * inputH)).normalized * currentMoveSpeed;
        //���
        velocity = Vector3.Lerp(oldVelocity, velocity, lerpSpeed * Time.deltaTime);

        if (velocity.magnitude < 0.001)
        {
            velocity = Vector3.zero;
        }
        //�Â����x���X�V
        oldVelocity = velocity;
    }
    /// <summary>
    /// �ړ��v�Z���ʂ�RigidBody�ɔ��f
    /// </summary>
    void MoveFixedUpdate()
    {
        var vel = velocity;
        vel.y = _rigidbody.velocity.y;

        _rigidbody.velocity = vel;
    }
}
