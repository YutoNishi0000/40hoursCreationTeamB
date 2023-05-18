using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static bool IsSpawn = true;
    public static GameObject target = null;
    //ターゲットオブジェクトのプレハブ
    [SerializeField] GameObject respawnTarget = null;
    // スポーン座標リスト
    [SerializeField] List<GameObject> _positionList = new List<GameObject>();
    // スポーンさせるオブジェクト
    [SerializeField] List<GameObject> _spawnObject = new List<GameObject>();
    // スポーンさせる座標
    Vector3 _spawnPosition;
    private void Awake()
    {
        Respawntarget();
    }
    void Update()
    {
        if (!IsSpawn)
        {
            return;
        }
        Debug.Log("通ってる");
        IsSpawn = false;
        StartCoroutine(destroy());
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
        // 0～Listの個数の範囲でランダムな整数値が返る
        _spawnPosition = _positionList[Random.Range(0, _positionList.Count)].transform.position;
        target = Instantiate(_spawnObject[Random.Range(0, _spawnObject.Count)],
                    _spawnPosition,
                    Quaternion.Euler(0, 0, 0));

        

    }
}
