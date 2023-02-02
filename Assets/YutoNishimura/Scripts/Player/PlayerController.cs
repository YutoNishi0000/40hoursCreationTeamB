using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Human
{
    private Camera mainCam;

    Vector3 targetDirection;        //移動する方向のベクトル
    Vector3 moveDirection = Vector3.zero;

    private CharacterController controller;

    public float speed;         //キャラクターの移動速度
    public float jumpSpeed;     //キャラクターのジャンプ力
    public float rotateSpeed;   //キャラクターの方向転換速度
    public float gravity;       //キャラにかかる重力の大きさ

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        rotateSpeed = 10f;
        gravity = 20f;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
    }

    #region 移動関連処理

    public override void MoveControl()
    {
        //★進行方向計算
        //キーボード入力を取得
        float v = Input.GetAxisRaw("Vertical");         //InputManagerの↑↓の入力       
        float h = Input.GetAxisRaw("Horizontal");       //InputManagerの←→の入力 

        //カメラの正面方向ベクトルからY成分を除き、正規化してキャラが走る方向を取得
        Vector3 forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Camera.main.transform.right; //カメラの右方向を取得

        //カメラの方向を考慮したキャラの進行方向を計算
        targetDirection = h * right + v * forward;

        moveDirection = Vector3.Scale(targetDirection, new Vector3(1, 0, 1)).normalized;
        moveDirection *= speed;

        //走行アニメーション管理
        AnimationControl(v, h);

        //回転処理
        RotationControl();

        //最終的な移動処理
        controller.Move(moveDirection * Time.deltaTime);
    }

    //オーバーライド関数
    public void AnimationControl(float vertical, float height)
    {
        //走行アニメーション管理
        if (vertical > .1 || vertical < -.1 || height > .1 || height < -.1) //(移動入力があると)
        {
            animator.SetFloat("Speed", 1f); //キャラ走行のアニメーション開始
        }
        else    //(移動入力が無いと)
        {
            animator.SetFloat("Speed", 0); //キャラ走行のアニメーション終了
        }
    }

    //回転処理
    void RotationControl()  //キャラクターが移動方向を変えるときの処理
    {
        Vector3 rotateDirection = moveDirection;
        rotateDirection.y = 0;

        //それなりに移動方向が変化する場合のみ移動方向を変える
        if (rotateDirection.sqrMagnitude > 0.01)
        {
            //緩やかに移動方向を変える
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.Slerp(transform.forward, rotateDirection, step);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    #endregion
}



//人型クラスの基底クラス
public class Human : MonoBehaviour
{
    //プレイヤーのインスタンス
    protected PlayerController playerInstance;

    private void Awake()
    {
        //ゲーム開始時プレイヤーインスタンスを取得
        playerInstance = FindObjectOfType<PlayerController>();
    }

    public virtual void MoveControl() { }
    public virtual void AnimationControl() { }
}
