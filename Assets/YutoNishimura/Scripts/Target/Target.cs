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
    private GameObject targetCamera = null;
    private float initialTargetSpeed;       //移動速度
    private GameObject pointParent = null;
    private GameObject rootParent1;
    private GameObject rootParent2;
    private GameObject rootParent3;
    private GameObject rootParent4;
    private bool isInsideCamera = false;
    private bool enableTakePicFlag;    //ターゲット撮影判定
    private const float disTargetShot = 7.0f;
    private readonly float minDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        enableTakePicFlag = false;
        agent = GetComponent<NavMeshAgent>();

        //int rootType = Random.Range(0, 4);
        //SetRootType((RespawTarget.RootType)rootType);

        initialTargetSpeed = agent.speed;

        //目標地点の間を継続的に移動
        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < minDistance)   
        {
            GoNextPoint();
        }
    }

    void GoNextPoint()
    {
        //地点が何も設定されていない場合
        if (points.Count == 0)
        {
            return;
        }

        //エージェントが現在設定された目標地点に行くように設定
        agent.destination = new Vector3(points[destPoint].transform.position.x, transform.position.y, points[destPoint].transform.position.z);

        //配列内の次の位置を目標地点に設定し、必要ならば出発地点に戻る
        destPoint = (destPoint + 1) % points.Count;
    }

    public void SetPoints(GameObject prent)
    {
        // 子オブジェクトの数を取得
        int childCount = prent.transform.childCount;

        // 子オブジェクトを順に取得する
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = prent.transform.GetChild(i);
            points.Add(childTransform.gameObject);
        }
    }

    public float GetTargetSpeed() { return agent.speed; }

    public float GetInitialTargetSpeed() { return initialTargetSpeed; }

    public void SetTargetSpeed(float speed) { agent.speed = speed; }

    public bool GetEnableTakePicFlag() { return enableTakePicFlag; }

    //ターゲットが生み出された直後に呼び出す（指定されたルートタイプによってセットするルートが異なる）
    public void SetRootType(RespawTarget.RootType type)
    {
        switch(type)
        {
            case RespawTarget.RootType.First:
                rootParent1 = GameObject.Find("Root1");
                SetPoints(rootParent1);
                break;
            case RespawTarget.RootType.Second:
                rootParent2 = GameObject.Find("Root2");
                SetPoints(rootParent2);
                break;
            case RespawTarget.RootType.Third:
                rootParent3 = GameObject.Find("Root3");
                SetPoints(rootParent3);
                break;
            case RespawTarget.RootType.Fourth:
                rootParent4 = GameObject.Find("Root4");
                SetPoints(rootParent4);
                break;
        }
    }
}
