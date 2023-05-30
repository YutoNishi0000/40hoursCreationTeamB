using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//AIに必要な関数などを定義した基底クラス
public class AIController : Actor
{
    protected NavMeshAgent agent;    //必ず継承先でGetComponentすること
    protected int destPoint;         //必ず継承先で初期化すること

    public virtual void GoNextPoint(List<GameObject> points)
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

    /// <summary>
    /// ポイントをセットする関数
    /// </summary>
    /// <param name="prent"></param>
    /// <param name="points"></param>
    /// <param name="startNum">何番目のポイントからスタートするか</param>
    public virtual void SetPoints(GameObject prent, List<GameObject> points, int startNum)
    {
        //points = null;
        // 子オブジェクトの数を取得
        int childCount = prent.transform.childCount;

        // 子オブジェクトを順に取得する
        for (int i = startNum; i < childCount; i++)
        {
            i = i % childCount;
            Transform childTransform = prent.transform.GetChild(i);
            points.Add(childTransform.gameObject);
        }
    }

    public virtual void SetRootType() { }
}
