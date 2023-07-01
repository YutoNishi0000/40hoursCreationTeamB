using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawTarget : MonoBehaviour
{
    //ルートの種類（全4種類）
    public enum RootType
    {
        First,
        Second,
        Third,
        Fourth
    }

    [SerializeField] private GameObject targetObj;        //ターゲットオブジェクト

    [SerializeField] private GameObject root1Pos;
    [SerializeField] private GameObject root2Pos;
    [SerializeField] private GameObject root3Pos;
    [SerializeField] private GameObject root4Pos;

    public static GameObject tempTarget;             //ターゲットのオブジェクトを保持するための変数

    private void Start()
    {
        SetTarget().Forget();
    }

    private void Update()
    {
        if(tempTarget == null)
        {
            SetTarget().Forget();
        }
    }

    public async UniTask SetTarget()
    {
        //1フレーム待つ（リスポーンの時にポストエフェクトをかけるため）
        await UniTask.DelayFrame(1);

        int rootType = Random.Range(0, 4);

        SEManager.Instance.PlayRespawn();

        //ルートタイプによってスポーンする位置が変わる
        switch ((RootType)rootType)
        {
            case RootType.First:
                tempTarget = Instantiate(targetObj, root1Pos.transform.position, Quaternion.identity);
                tempTarget.GetComponent<Target>().SetRootType(RootType.First);
                break;
            case RootType.Second:
                tempTarget = Instantiate(targetObj, root2Pos.transform.position, Quaternion.identity);
                tempTarget.GetComponent<Target>().SetRootType(RootType.Second);
                break;
            case RootType.Third:
                tempTarget = Instantiate(targetObj, root3Pos.transform.position, Quaternion.identity);
                tempTarget.GetComponent<Target>().SetRootType(RootType.Third);
                break;
            case RootType.Fourth:
                tempTarget = Instantiate(targetObj, root4Pos.transform.position, Quaternion.identity);
                tempTarget.GetComponent<Target>().SetRootType(RootType.Fourth);
                break;
        }
    }

    public static void RespawnTarget()
    {
        Destroy(tempTarget);
    }

    //フィールド上に存在しているターゲットオブジェクトを取得
    public static GameObject GetCurrentTargetObj() { return tempTarget; }
}
