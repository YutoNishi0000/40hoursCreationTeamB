using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//自身の周辺をうろつくようにする
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
    private int spawnNumber;    //スポーンナンバー
    private List<GameObject> root;
    private readonly float minDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        root = new List<GameObject>();
        //自身のスポーンされた位置を記憶する
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
        
        //常に何かしらのアニメーションを再生する
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

    //オーバーロード関数
    //改変する必要あり（関数内にSetPointsが入るように）
    public void SetRootType(int num, GameObject[] parentWonderPoints)
    {
        SetPoints(parentWonderPoints[(int)GetRootType(num)], root, GetStartPoint(num));
    }

    //スポーン番号からルートタイプを取得する関数
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

    //目的地のリストの何番目からスタートすればよいのかを記述した関数
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
