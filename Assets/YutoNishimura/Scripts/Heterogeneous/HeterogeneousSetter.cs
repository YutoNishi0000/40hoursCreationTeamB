using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//異質なもの
public class HeterogeneousSetter : MonoBehaviour
{
    [SerializeField] private GameObject[] points;   //現時点では8箇所と仮定
    [SerializeField] private GameObject ObjectA;
    [SerializeField] private GameObject ObjectB;
    [SerializeField] private GameObject ObjectC;
    [SerializeField] private GameObject ObjectD;
    [SerializeField] private GameObject[] parentWonderPoints;       //ルートポイントの親オブジェクトを入れてください（動く異質なもの）
    private Queue<GameObject> queue;
    public List<GameObject> objSpawnPos;
    private List<int> pos;
    private int rand;                    //ランダムな数字
    private int fieldObjectsNum;         //フィールド内にある異質なものの個数
    private ScreenShot screen;
    private readonly int numStrangeObjInField = 8;
    private readonly static float respawnCoolTime = 10;    //異質なものが再生成されるまでのクールタイム
    private static float coolTime;
    public static int numRand;
    void Start()
    {
        coolTime = 0;
        screen = GameObject.FindObjectOfType<ScreenShot>();
        fieldObjectsNum = 0;
        objSpawnPos = new List<GameObject>();
        queue = new Queue<GameObject>();
        pos = new List<int>();
        queue.Enqueue(ObjectA);
        queue.Enqueue(ObjectB);
        queue.Enqueue(ObjectC);
        queue.Enqueue(ObjectD);

        //GameObject型リスト内8箇所全てににnullを入れて初期化する
        for (int i = 0; i < points.Count(); i++)
        {
            objSpawnPos.Add(null);
        }

        InitialSetObjects();
    }

    void LateUpdate()
    {
        SetObjects();
        screen.SetList(objSpawnPos);
    }

    //ゲーム開始時８か所あるポイントにランダムに３か所オブジェクトを配置する
    private void InitialSetObjects()
    {
        List<int> rnd = new List<int>();

        for (int i = 0; i < numStrangeObjInField; i++)
        {
            rnd.Add(Random.Range(0, points.Count()));

            for(int j = 0; j < rnd.Count(); j++)
            {
                while(rnd[i] == rnd[j] && i != j)
                {
                    rnd[i] = Random.Range(0, points.Count());
                }
            }

            objSpawnPos[rnd[i]] = Instantiate(GetNextObject(), points[rnd[i]].transform.position, Quaternion.identity);

            numRand = rnd[i];

            if (objSpawnPos[rnd[i]].GetComponentInChildren<MetalonController>())
            {
                Debug.Log("セット");
                objSpawnPos[rnd[i]].GetComponent<MetalonController>().SetRootType();
                objSpawnPos[rnd[i]].GetComponent<MetalonController>().SetSpawnNumber(rnd[i]);
                objSpawnPos[rnd[i]].GetComponent<MetalonController>().SetFinishedSetRootFlag(true);
            }
        }
    }

    //かぶらないように空いている異質なものの生成ポイントに生成する
    public void SetObjects()
    {
        //クールタイムを設置
        coolTime -= Time.deltaTime;

        if(coolTime > 0)
        {
            return;
        }
        else
        {
            coolTime = 0;
        }

        //今異質なものが何個配置されているのかを確認
        for(int i = 0; i < points.Count(); i++)
        {
            if (objSpawnPos[i] != null)
            {
                fieldObjectsNum++;
            }
        }

        //足りない分を補うような形で異質なものを動的に配置する
        for (int j = 0; j < numStrangeObjInField - fieldObjectsNum; j++)
        {
            int rand = Random.Range(0, points.Count());

            while (objSpawnPos[rand] != null)
            {
                rand = Random.Range(0, points.Count());
            }

            objSpawnPos[rand] = Instantiate(GetNextObject(), points[rand].transform.position, GetNextObject().transform.rotation);

            numRand = rand;

            if (objSpawnPos[rand].GetComponent<MetalonController>())
            {
                Debug.Log("セット");
                objSpawnPos[rand].GetComponent<MetalonController>().SetRootType();
                objSpawnPos[rand].GetComponent<MetalonController>().SetSpawnNumber(rand);
                objSpawnPos[rand].GetComponent<MetalonController>().SetFinishedSetRootFlag(true);
            }
        }

        fieldObjectsNum = 0;
    }

    //クールタイム発生
    public static void CoolTime()
    {
        coolTime = respawnCoolTime;
    }

    //キューを使用して次の要素のオブジェクトをキューにセット、取得する関数
    private GameObject GetNextObject()
    {
        if (queue.Count == 0)
        {
            return null;
        }

        GameObject obj  = queue.Peek();

        queue.Enqueue(obj);

        return queue.Dequeue();
    }

    public ref List<GameObject> GetObjSpawnPos() { return ref objSpawnPos; }
}