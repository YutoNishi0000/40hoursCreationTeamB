using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//自身の周辺をうろつくようにする
public class MetalonController : Actor
{
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
    public List<GameObject> points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private GameObject rootParent1;
    private GameObject rootParent2;
    private GameObject rootParent3;
    private GameObject rootParent4;
    private GameObject rootParent5;
    private GameObject rootParent6;
    private GameObject rootParent7;
    private const float disTargetShot = 7.0f;
    private readonly float minDistance = 0.5f;
    private bool finishedSetRoot;                //ルート設定が完了したかどうか
    private readonly int rootNum = 7;               //ルートが何種類あるか
    private int spawnNum;                         //スポーン番号
    private SubRootType subRootType;

    // Start is called before the first frame update
    void Start()
    {
        spawnNum = 0;
        destPoint = 0;
        agent = GetComponent<NavMeshAgent>();
        points = new List<GameObject>();
        finishedSetRoot = false;
        //目標地点の間を継続的に移動
        agent.autoBraking = false;
        animator = GetComponent<Animator>();
        SetRootType();
        subRootType = new SubRootType();
    }

    // Update is called once per frame
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

        //ルートの設定が終わるまで待機
        if(finishedSetRoot)
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
    public void SetRootType()
    {
        SubRootType type = (SubRootType)Random.Range(0, rootNum);

        switch (type)
        {
            case SubRootType.First:
                rootParent1 = GameObject.Find("WonderPoint0");
                SetPoints(rootParent1);
                break;
            case SubRootType.Second:
                rootParent2 = GameObject.Find("WonderPoint1");
                SetPoints(rootParent2);
                break;
            case SubRootType.Third:
                rootParent3 = GameObject.Find("WonderPoint2");
                SetPoints(rootParent3);
                break;
            case SubRootType.Fourth:
                rootParent4 = GameObject.Find("WonderPoint3");
                SetPoints(rootParent4);
                break;
            case SubRootType.Fifth:
                rootParent5 = GameObject.Find("WonderPoint4");
                SetPoints(rootParent5);
                break;
            case SubRootType.Sixth:
                rootParent6 = GameObject.Find("WonderPoint5");
                SetPoints(rootParent6);
                break;
            case SubRootType.Seventh:
                rootParent7 = GameObject.Find("WonderPoint6");
                SetPoints(rootParent7);
                break;
        }
    }

    //スポーン番号からルートタイプを取得する関数
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

    //目的地のリストの何番目からスタートすればよいのかを返す
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
        subRootType = GetRootType(num);
        destPoint = GetStartPoint(num);
    }

    public void SetFinishedSetRootFlag(bool flag) { finishedSetRoot = flag; }
}


//public enum SubRootType
//{
//    First,
//    Second,
//    Third,
//    Fourth,
//    Fifth,
//    Sixth,
//    Seventh
//}

//public List<GameObject> points;
//private int destPoint = 0;
//private NavMeshAgent agent;
//private GameObject rootParent1;
//private GameObject rootParent2;
//private GameObject rootParent3;
//private GameObject rootParent4;
//private GameObject rootParent5;
//private GameObject rootParent6;
//private GameObject rootParent7;
//private const float disTargetShot = 7.0f;
//private readonly float minDistance = 0.5f;

//// Start is called before the first frame update
//void Start()
//{
//    agent = GetComponent<NavMeshAgent>();

//    //目標地点の間を継続的に移動
//    agent.autoBraking = false;

//    SetRootType();
//}

//// Update is called once per frame
//void Update()
//{
//    if (!agent.pathPending && agent.remainingDistance < minDistance)
//    {
//        GoNextPoint();
//    }
//}

//void GoNextPoint()
//{
//    //地点が何も設定されていない場合
//    if (points.Count == 0)
//    {
//        return;
//    }

//    //エージェントが現在設定された目標地点に行くように設定
//    agent.destination = new Vector3(points[destPoint].transform.position.x, transform.position.y, points[destPoint].transform.position.z);

//    //配列内の次の位置を目標地点に設定し、必要ならば出発地点に戻る
//    destPoint = (destPoint + 1) % points.Count;
//}

//public void SetPoints(GameObject prent)
//{
//    // 子オブジェクトの数を取得
//    int childCount = prent.transform.childCount;

//    // 子オブジェクトを順に取得する
//    for (int i = 0; i < childCount; i++)
//    {
//        Transform childTransform = prent.transform.GetChild(i);
//        points.Add(childTransform.gameObject);
//    }
//}

////ターゲットが生み出された直後に呼び出す
//public void SetRootType()
//{
//    SubRootType type = (SubRootType)Random.Range(0, points.Count);

//    switch (type)
//    {
//        case SubRootType.First:
//            rootParent1 = GameObject.Find("WonderPoint0");
//            SetPoints(rootParent1);
//            break;
//        case SubRootType.Second:
//            rootParent2 = GameObject.Find("WonderPoint1");
//            SetPoints(rootParent2);
//            break;
//        case SubRootType.Third:
//            rootParent3 = GameObject.Find("WonderPoint2");
//            SetPoints(rootParent3);
//            break;
//        case SubRootType.Fourth:
//            rootParent4 = GameObject.Find("WonderPoint3");
//            SetPoints(rootParent4);
//            break;
//        case SubRootType.Fifth:
//            rootParent5 = GameObject.Find("WonderPoint4");
//            SetPoints(rootParent5);
//            break;
//        case SubRootType.Sixth:
//            rootParent6 = GameObject.Find("WonderPoint5");
//            SetPoints(rootParent6);
//            break;
//        case SubRootType.Seventh:
//            rootParent7 = GameObject.Find("WonderPoint6");
//            SetPoints(rootParent7);
//            break;
//    }
//}























//private enum RootType
//{
//    First,
//    Second,
//    Third,
//    Fourth,
//    Fifth,
//    Sixth,
//    Seventh
//}

//private GameObject rootParent1;
//private GameObject rootParent2;
//private GameObject rootParent3;
//private GameObject rootParent4;
//private GameObject rootParent5;
//private GameObject rootParent6;
//private GameObject rootParent7;

//private Vector3 initializeSpawnPos;
//private Animator animator;
//private int spawnNumber;    //スポーンナンバー
//private List<GameObject> root;
//private readonly float minDistance = 0.5f;
//private NavMeshAgent agent;
//int destPoint;

//// Start is called before the first frame update
//void Start()
//{
//    animator = GetComponent<Animator>();
//    //自身のスポーンされた位置を記憶する
//    initializeSpawnPos = transform.position;
//    agent = GetComponent<NavMeshAgent>();
//    destPoint = 0;
//    root = new List<GameObject>();
//}

//// Update is called once per frame
//void Update()
//{
//    if (!agent.pathPending && agent.remainingDistance < minDistance)
//    {
//        GoNextPoint();
//    }
//}

///// <summary>
///// ポイントをセットする関数
///// </summary>
///// <param name="prent"></param>
///// <param name="points"></param>
///// <param name="startNum">何番目のポイントからスタートするか</param>
//public void SetPoints(GameObject prent)
//{
//    //points = null;
//    // 子オブジェクトの数を取得
//    int childCount = prent.transform.childCount;

//    // 子オブジェクトを順に取得する
//    for (int i = 0; i < childCount; i++)
//    {
//        Transform childTransform = prent.transform.GetChild(i);
//        root.Add(childTransform.gameObject);
//    }
//}

//void GoNextPoint()
//{
//    //地点が何も設定されていない場合
//    if (root.Count == 0)
//    {
//        return;
//    }

//    //エージェントが現在設定された目標地点に行くように設定
//    agent.destination = new Vector3(root[destPoint].transform.position.x, transform.position.y, root[destPoint].transform.position.z);

//    //配列内の次の位置を目標地点に設定し、必要ならば出発地点に戻る
//    destPoint = (destPoint + 1) % root.Count;
//}

////オーバーロード関数
////改変する必要あり（関数内にSetPointsが入るように）
//public void SetRootType()
//{
//    RootType type = GetRootType(HeterogeneousSetter.numRand);

//    switch (type)
//    {
//        case RootType.First:
//            rootParent1 = GameObject.Find("WonderPoint0");
//            destPoint = GetStartPoint(HeterogeneousSetter.numRand);
//            SetPoints(rootParent1);
//            break;
//        case RootType.Second:
//            rootParent2 = GameObject.Find("WonderPoint1");
//            destPoint = GetStartPoint(HeterogeneousSetter.numRand);
//            SetPoints(rootParent2);
//            break;
//        case RootType.Third:
//            rootParent3 = GameObject.Find("WonderPoint2");
//            destPoint = GetStartPoint(HeterogeneousSetter.numRand);
//            SetPoints(rootParent3);
//            break;
//        case RootType.Fourth:
//            rootParent4 = GameObject.Find("WonderPoint3");
//            destPoint = GetStartPoint(HeterogeneousSetter.numRand);
//            SetPoints(rootParent4);
//            break;
//        case RootType.Fifth:
//            rootParent5 = GameObject.Find("WonderPoint4");
//            destPoint = GetStartPoint(HeterogeneousSetter.numRand);
//            SetPoints(rootParent5);
//            break;
//        case RootType.Sixth:
//            rootParent6 = GameObject.Find("WonderPoint5");
//            destPoint = GetStartPoint(HeterogeneousSetter.numRand);
//            SetPoints(rootParent6);
//            break;
//        case RootType.Seventh:
//            rootParent7 = GameObject.Find("WonderPoint6");
//            destPoint = GetStartPoint(HeterogeneousSetter.numRand);
//            SetPoints(rootParent7);
//            break;
//    }
//}

////スポーン番号からルートタイプを取得する関数
//private RootType GetRootType(int num)
//{
//    switch(num)
//    {
//        case 0:
//            return RootType.First;
//        case 3:
//            return RootType.Second;
//        case 7:
//            return RootType.Third;
//        case 9:
//            return RootType.Fourth;
//        case 5:
//        case 6:
//            return RootType.Fifth;
//        case 2:
//        case 8:
//            return RootType.Sixth;
//        default:
//            return RootType.Seventh;
//    }
//}

////目的地のリストの何番目からスタートすればよいのかを記述した関数
//private int GetStartPoint(int num)
//{
//    switch(num)
//    {
//        case 1:
//            return 2;
//        case 5:
//            return 1;
//        case 10:
//            return 5;
//        default:
//            return 0;
//    }
//}