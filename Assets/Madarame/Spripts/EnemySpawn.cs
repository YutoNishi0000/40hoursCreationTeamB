using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // スポーン座標リスト
    [SerializeField] List<GameObject> _positionList = new List<GameObject>();
    // 生成の有無
    bool _isSpawn = true;
    // スポーンさせる座標
    Vector3 _spawnPosition;
    // スポーンさせるオブジェクト
    [SerializeField] List<GameObject> _spawnObject = new List<GameObject>();

    void Start()
    {
         
    }

    void Update()
    {
        //if(_isSpawn)
        //{
            // 0～Listの個数の範囲でランダムな整数値が返る
            _spawnPosition = _positionList[Random.Range(0, _positionList.Count)].transform.position;
            Instantiate(_spawnObject[Random.Range(0, _spawnObject.Count)], 
                        _spawnPosition,
                        Quaternion.Euler(0, 0, 0));
         
           // _isSpawn = false;
        //}
    }
}
