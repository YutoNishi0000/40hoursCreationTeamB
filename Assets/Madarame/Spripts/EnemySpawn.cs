using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // �X�|�[�����W���X�g
    [SerializeField] List<GameObject> _positionList = new List<GameObject>();
    // �����̗L��
    bool _isSpawn = true;
    // �X�|�[����������W
    Vector3 _spawnPosition;
    // �X�|�[��������I�u�W�F�N�g
    [SerializeField] List<GameObject> _spawnObject = new List<GameObject>();

    void Start()
    {
         
    }

    void Update()
    {
        //if(_isSpawn)
        //{
            // 0�`List�̌��͈̔͂Ń����_���Ȑ����l���Ԃ�
            _spawnPosition = _positionList[Random.Range(0, _positionList.Count)].transform.position;
            Instantiate(_spawnObject[Random.Range(0, _spawnObject.Count)], 
                        _spawnPosition,
                        Quaternion.Euler(0, 0, 0));
         
           // _isSpawn = false;
        //}
    }
}
