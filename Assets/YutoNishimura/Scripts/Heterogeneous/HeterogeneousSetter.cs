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
    private Queue<GameObject> queue;
    public List<GameObject> objSpawnPos;   //Don't Touch!!
    private List<int> pos;
    private int rand;                    //ランダムな数字
    private int fieldObjectsNum;         //フィールド内にある異質なものの個数
    private ScreenShot screen;

    void Start()
    {
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

    void Update()
    {
        Debug.Log("1isitunamono" + objSpawnPos.Count);
        SetObjects();
        screen.SetList(objSpawnPos);
    }

    //ゲーム開始時８か所あるポイントにランダムに３か所オブジェクトを配置する
    private void InitialSetObjects()
    {
        List<int> rnd = new List<int>();

        for (int i = 0; i < 8; i++)
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
        }
    }

    private void SetObjects()
    {
        //今異質なものが何個配置されているのかを確認
        for(int i = 0; i < points.Count(); i++)
        {
            if (objSpawnPos[i] != null)
            {
                fieldObjectsNum++;
            }
        }

        //消した異質なもののデータが完全に削除されたらここから下の処理を再開する
        for (int i = 0; i < screen.GetDestroyList().Count; i++)
        {
            //完全に消えていなかったらreturn
            if (objSpawnPos[screen.GetDestroyList()[i]] != null)
            {
                return;
            }
        }

        //ここにきているということは、完全に撮影された異質なものが削除されたことになるのでリストを初期化する
        screen.SetDestroyList(null);

        //足りない分を補うような形で異質なものを動的に配置する
        for (int j = 0; j < 8 - fieldObjectsNum; j++)
        {
            int rand = Random.Range(0, points.Count());

            while (objSpawnPos[rand] != null)
            {
                rand = Random.Range(0, points.Count());
            }

            objSpawnPos[rand] = Instantiate(GetNextObject(), points[rand].transform.position, Quaternion.identity);
        }

        fieldObjectsNum = 0;
    }

    //キューを使用して次のオブジェクトをキューにセット、取得する関数
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