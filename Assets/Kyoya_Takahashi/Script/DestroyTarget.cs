using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    public static GameObject target = null;
    //ターゲットオブジェクトのプレハブ
    [SerializeField] GameObject respawnTarget = null;
    private void Awake()
    {
        Respawntarget();
    }
    private void LateUpdate()
    {
        if (!GameManager.Instance.IsPhoto)
        {
            return;
        }
        if(ScoreManger.Score == 0)
        {
            return;
        }
        Debug.Log("通ってる");
        StartCoroutine(destroy());
        GameManager.Instance.IsPhoto = false;
    }
    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(target);
        yield return new WaitForSeconds(1);
        //Debug.Log("通ってる");
        Respawntarget();
    }
    private void Respawntarget()
    {
        //ターゲットが存在してたらターゲット生成
        target = Instantiate(respawnTarget,
            this.transform.position,
            Quaternion.Euler(0.0f, 0.0f, 0.0f));
        
    }
}
