using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TargetController : Human
{
    private NavMeshAgent _navmeshAgent;    //ナビメッシュエージェント
    private Animator _animator;
    public GameObject[] passingPoints;     //市民の通過ポイント
    private bool _passAllPoints;           //全ての通貨ポイントを通過したかどうか
    private int _pointIndex;
    private float _lockedOnTime;
    [SerializeField] private float RECOGNIZE_TIME = 3;
    private bool _moveLock;               //移動を制限するためのフラグ

    //=====================================================================
    // 通過ポイントに関して
    //=====================================================================
    //
    //・向かいたい順番にGameobject配列にGameObjectを入れて下さい
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
        //配列の一番最初の要素も目的地にしたければ下の処理のコメントアウトを外してもう一行下の処理をコメントアウトしてください
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

        //アニメーション関連処理
        AnimationControl(_navmeshAgent.velocity.magnitude);

        //経路を持っているなら処理しない
        if (_navmeshAgent.hasPath)
        {
            return;
        }

        //現在セットされている目的地の配列番号が配列の長さから１引いた数より小さい且つ_passAllPointsがfalseだったら
        if (_pointIndex < passingPoints.Length - 1 && !_passAllPoints)
        {
            //配列にある次の要素を格納
            _navmeshAgent.SetDestination(passingPoints[_pointIndex++].transform.position);
        }
        else
        {
            //ここに入ってきたということは全てのポイントを回ってということなので_passAllPointsをtrueにする
            _passAllPoints = true;

            //目的地は配列の一個前の要素を指定して来た道を戻るようにする
            _navmeshAgent.SetDestination(passingPoints[_pointIndex--].transform.position);

            //往復して元居た位置に戻ってきたら
            if (_pointIndex == 0)
            {
                //_passAllPointsをfalseにしてこの分岐から抜ける
                _passAllPoints = false;
            }
        }
    }

    public void AnimationControl(float velocity)
    {
        //引数として渡された値が0より大きければ
        if (velocity > .1)
        {
            //アニメーションを再生する
            _animator.SetFloat("Speed", 1);
        }
        //引数として渡された値が0以下であれば
        else
        {
            //アニメーションを止める
            _animator.SetFloat("Speed", 0);
        }
    }

    /// <summary>
    /// プレイヤーに視点をロックされていたらカウントを開始し、プレイヤーの方向を向くようにする
    /// </summary>
    void CheckPlayer()
    {
        if (RaycastController.Lockon)
        {
            _lockedOnTime += Time.deltaTime;

            if (_lockedOnTime > RECOGNIZE_TIME)
            {
                //移動ロックオン
                _moveLock = true;

                //y軸だけプレイヤーの方向を向かせたい
                Vector3 target = playerInstance.gameObject.transform.position;
                target.y = this.transform.position.y;
                this.transform.LookAt(target);
                Debug.Log("プレイヤーに気づいた");
            }
        }
    }

    /// <summary>
    /// 移動、アニメーション全ての処理を止める
    /// </summary>
    void StopMove()
    {
        _navmeshAgent.isStopped = true;     //移動をやめさせる
        AnimationControl(0);                //アニメーション再生を止める
    }
}
