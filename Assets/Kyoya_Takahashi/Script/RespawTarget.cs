using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawTarget : MonoBehaviour
{
    public enum RootType
    {
        First,
        Second,
        Third,
        Fourth
    }

    //ターゲットオブジェクト
    [SerializeField] private GameObject targetObj;

    [SerializeField] private GameObject root1Pos;
    [SerializeField] private GameObject root2Pos;
    [SerializeField] private GameObject root3Pos;
    [SerializeField] private GameObject root4Pos;

    public static GameObject tempTarget;

    private void Start()
    {
        SetTarget();
    }

    private void Update()
    {
        if(tempTarget == null)
        {
            SetTarget();
        }
    }

    public void SetTarget()
    {
        int rootType = Random.Range(0, 4);

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

    public static GameObject GetCurrentTargetObj() { return tempTarget; }
}
