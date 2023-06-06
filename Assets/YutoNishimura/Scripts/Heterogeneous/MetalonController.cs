using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//自身の周辺をうろつくようにする
public class MetalonController : Actor
{
    //メタロン（動く異質なもの）のルートの種類
    public enum SubRootType
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh
    }

    private Animator animator;
    public List<GameObject> points = new List<GameObject>();
    private int destPoint = 0;
    private NavMeshAgent agent;
    private List<GameObject> parentPoints;
    private readonly float minDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        parentPoints = new List<GameObject>();
        agent = GetComponent<NavMeshAgent>();
        //目標地点の間を継続的に移動
        agent.autoBraking = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < minDistance)
        {
            GoNextPoint();
        }

        animator.SetFloat("Run", agent.velocity.magnitude);
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

    //ターゲットが生み出された直後に呼び出す
    public void SetRootType(SubRootType type)
    {
        switch (type)
        {
            case SubRootType.First:
                SetPoints(parentPoints[(int)SubRootType.First]);
                break;
            case SubRootType.Second:
                SetPoints(parentPoints[(int)SubRootType.Second]);
                break;
            case SubRootType.Third:
                SetPoints(parentPoints[(int)SubRootType.Third]);
                break;
            case SubRootType.Fourth:
                SetPoints(parentPoints[(int)SubRootType.Fourth]);
                break;
            case SubRootType.Fifth:
                SetPoints(parentPoints[(int)SubRootType.Fifth]);
                break;
            case SubRootType.Sixth:
                SetPoints(parentPoints[(int)SubRootType.Sixth]);
                break;
            case SubRootType.Seventh:
                SetPoints(parentPoints[(int)SubRootType.Seventh]);
                break;
        }
    }

    //スポーン位置によってセットするルートが変わるのでスポーン番号に応じてルートタイプを取得する関数
    private SubRootType GetRootType(int num)
    {
        switch (num)
        {
            case 0:
                return SubRootType.First;
            case 3:
                return SubRootType.Second;
            case 7:
                return SubRootType.Third;
            case 9:
                return SubRootType.Fourth;
            case 5:
            case 6:
                return SubRootType.Fifth;
            case 2:
            case 8:
                return SubRootType.Sixth;
            default:
                return SubRootType.Seventh;
        }
    }

    //目的地のルートリストの何番目からスタートすればよいのかを返す
    public int GetStartPoint(int num)
    {
        switch (num)
        {
            case 0:
                return 7;
            case 1:
                return 5;
            case 4:
                return 13;
            case 5:
                return 3;
            case 6:
                return 7;
            case 7:
                return 5;
            case 8:
                return 7;
            case 9:
                return 8;
            case 10:
                return 10;
            default:
                return 0;
        }
    }

    public void SetSpawnNumber(int num) 
    {
        SetRootType(GetRootType(num));
        destPoint = GetStartPoint(num);
    }

    public void SetWonderParentPoints(List<GameObject> points) { parentPoints = points; }
}