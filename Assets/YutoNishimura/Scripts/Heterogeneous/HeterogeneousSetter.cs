using Cysharp.Threading.Tasks;
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
    [SerializeField] private List<GameObject> parentWonderPoints;       //ルートポイントの親オブジェクトを入れてください（動く異質なもの）
    private Queue<GameObject> queue;
    public List<GameObject> objSpawnPos;
    private List<int> pos;
    private int rand;                    //ランダムな数字
    private int fieldObjectsNum;         //フィールド内にある異質なものの個数
    private ScreenShot screen;
    private readonly int numStrangeObjInField = Config.numSubTargetInField;
    private readonly static float respawnCoolTime = Config.subSubTargetGenerationCoolTime;    //異質なものが再生成されるまでのクールタイム
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

            if (objSpawnPos[rnd[i]].GetComponentInChildren<MetalonController>())
            {
                SetMetalonConfig(rnd[i], parentWonderPoints).Forget();
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

            if (objSpawnPos[rand].GetComponent<MetalonController>())
            {
                SetMetalonConfig(rand, parentWonderPoints).Forget();
            }
        }

        fieldObjectsNum = 0;
    }

    private async UniTask SetMetalonConfig(int num, List<GameObject> parentPoints)
    {
        await UniTask.Delay(3000);
        Debug.Log("セット");
        MetalonController metaron = objSpawnPos[num].GetComponent<MetalonController>();
        //ルート情報が入った親オブジェクトをセット
        metaron.SetWonderParentPoints(parentPoints);
        //スポーンナンバーをセット
        metaron.SetSpawnNumber(num);
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