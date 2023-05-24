using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawTarget : MonoBehaviour
{
    //ターゲットが存在しているか
    public static bool isExistsTarget = true;
    //ターゲットオブジェクト
    [SerializeField] GameObject target = null;

    private void LateUpdate()
    {
        if(isExistsTarget)
        {
            return;
        }
        //ターゲットが存在してたらターゲット生成
        Instantiate(target,
            this.transform.position,
            Quaternion.Euler(0.0f, 0.0f, 0.0f));
        isExistsTarget = true;
    }
}
