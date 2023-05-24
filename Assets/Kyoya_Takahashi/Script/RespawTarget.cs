using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawTarget : MonoBehaviour
{
    //ルートの種類
    private enum Root
    {
        First,
        Second,
        Third,
        Fourth
    }

    [Header("ターゲットオブジェクト")]
    [SerializeField] private GameObject targetObj;
    [Header("ターゲットのリスポーン位置（ルートの順番に合わせてください）")]
    [SerializeField] private List<GameObject> targetSpawnPos;
    [Header("ターゲット第一ルート")]
    [SerializeField] private List<GameObject> root1;
    [Header("ターゲット第二ルート")]
    [SerializeField] private List<GameObject> root2;
    [Header("ターゲット第三ルート")]
    [SerializeField] private List<GameObject> root3;
    [Header("ターゲット第四ルート")]
    [SerializeField] private List<GameObject> root4;

    private Root root;
    private GameObject tmpTarget;

    private void Start()
    {
        targetSpawnPos = new List<GameObject>();
        root = new Root();
        InitialSpawnTarget();
    }

    private void LateUpdate()
    {
        if(tmpTarget == null)
        {
            SetTarget();
        }
    }

    //private void LateUpdate()
    //{
    //    if(isExistsTarget)
    //    {
    //        return;
    //    }
    //    //ターゲットが存在してたらターゲット生成
    //    Instantiate(target,
    //        this.transform.position,
    //        Quaternion.Euler(0.0f, 0.0f, 0.0f));
    //    isExistsTarget = true;
    //}

    public void InitialSpawnTarget()
    {
        tmpTarget = Instantiate(targetObj, targetSpawnPos[(int)Root.First].transform.position, Quaternion.identity);
        tmpTarget.GetComponent<Target>().SetTargetRoot(root1);
    }

    //ターゲットをリスポーン、ターゲットのルートを設定する関数
    public void SetTarget()
    {
        root = (Root)Random.Range(0, 4);

        switch (root)
        {
            case Root.First:
                tmpTarget = Instantiate(targetObj, targetSpawnPos[(int)Root.First].transform.position, Quaternion.identity);
                tmpTarget.GetComponent<Target>().SetTargetRoot(root1);
                break;
            case Root.Second:
                tmpTarget = Instantiate(targetObj, targetSpawnPos[(int)Root.Second].transform.position, Quaternion.identity);
                tmpTarget.GetComponent<Target>().SetTargetRoot(root2);
                break;
            case Root.Third:
                tmpTarget = Instantiate(targetObj, targetSpawnPos[(int)Root.Third].transform.position, Quaternion.identity);
                tmpTarget.GetComponent<Target>().SetTargetRoot(root3);
                break;
            case Root.Fourth:
                tmpTarget = Instantiate(targetObj, targetSpawnPos[(int)Root.Fourth].transform.position, Quaternion.identity);
                tmpTarget.GetComponent<Target>().SetTargetRoot(root4);
                break;
        }
    }
}