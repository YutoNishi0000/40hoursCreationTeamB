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
    private List<GameObject> objSpawnPos;
    private int rand;                    //ランダムな数字
    private int fieldObjectsNum;         //フィールド内にある異質なものの個数

    void Start()
    {
        fieldObjectsNum = 0;
        objSpawnPos = new List<GameObject>();
        queue = new Queue<GameObject>();
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
        SetObjects();
    }

    //ゲーム開始時８か所あるポイントにランダムに３か所オブジェクトを配置する
    private void InitialSetObjects()
    {
        for(int i = 0; i < 3; i++)
        {
            int rnd = Random.Range(0, 8);

            while(rand == rnd)
            {
                rnd = Random.Range(0, 8);
            }

            objSpawnPos[rnd] = Instantiate(GetNextObject(), points[rnd].transform.position, Quaternion.identity);
        }
    }

    private void SetObjects()
    {
        for(int i = 0; i < points.Count(); i++)
        {
            if (objSpawnPos[i] != null)
            {
                fieldObjectsNum++;
            }
            else
            {
                if(fieldObjectsNum >= 3)
                {
                    return;
                }

                objSpawnPos[i] = Instantiate(GetNextObject(), points[i].transform.position, Quaternion.identity);
            }
        }
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
}