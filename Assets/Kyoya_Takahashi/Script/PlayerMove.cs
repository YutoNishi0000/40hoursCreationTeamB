using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //移動速度
    [SerializeField] float walkSpeed = 5.0f;
    [SerializeField] float runSpeed = 10.0f;
    //方向キー入力値
    private float inputH = 0;
    private float inputV = 0;
    //速度
    private Vector3 velocity = Vector3.zero;
    private Vector3 oldVelocity = Vector3.zero;
    //現在の移動速度
    private float currentMoveSpeed = 0;
    //コンポーネント
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
    /// キー入力の取得
    /// </summary>
    private void InputUpdate()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");
    }
    /// <summary>
    /// 移動計算
    /// </summary>
    private void MoveUpdate()
    {
        float lerpSpeed = 5.0f;
        currentMoveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        //移動方向と移動量の算出
        velocity = ((transform.forward * inputV) + (transform.right * inputH)).normalized * currentMoveSpeed;
        //補間
        velocity = Vector3.Lerp(oldVelocity, velocity, lerpSpeed * Time.deltaTime);

        if (velocity.magnitude < 0.001)
        {
            velocity = Vector3.zero;
        }
        //古い速度を更新
        oldVelocity = velocity;
    }
    /// <summary>
    /// 移動計算結果をRigidBodyに反映
    /// </summary>
    void MoveFixedUpdate()
    {
        var vel = velocity;
        vel.y = _rigidbody.velocity.y;

        _rigidbody.velocity = vel;
    }
}
