using System.Collections;
using System.Collections.Generic;
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
    private int rand;                    //ランダムな数字

    void Start()
    {
        queue = new Queue<GameObject>();
        queue.Enqueue(ObjectA);
        queue.Enqueue(ObjectB);
        queue.Enqueue(ObjectC);
        queue.Enqueue(ObjectD);

        SetObjects();
    }

    void Update()
    {

    }

    //ゲーム開始時８か所あるポイントにランダムに３か所オブジェクトを配置する
    private void SetObjects()
    {
        for(int i = 0; i < 3; i++)
        {
            int rnd = Random.Range(0, 8);

            while(rand == rnd)
            {
                rnd = Random.Range(0, 8);
            }

            Instantiate(GetNextObject(), points[rnd].transform.position, Quaternion.identity);
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