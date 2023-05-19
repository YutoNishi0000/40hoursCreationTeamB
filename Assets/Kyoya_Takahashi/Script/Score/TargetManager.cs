using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static bool IsSpawn = true;
    public static GameObject target = null;
    //�^�[�Q�b�g�I�u�W�F�N�g�̃v���n�u
    [SerializeField] GameObject respawnTarget = null;
    // �X�|�[�����W���X�g
    [SerializeField] List<GameObject> _positionList = new List<GameObject>();
    // �X�|�[��������I�u�W�F�N�g
    [SerializeField] List<GameObject> _spawnObject = new List<GameObject>();
    // �X�|�[����������W
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
        Debug.Log("�ʂ��Ă�");
        IsSpawn = false;
        StartCoroutine(destroy());
    }
    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(target);
        yield return new WaitForSeconds(1);
        //Debug.Log("�ʂ��Ă�");
        Respawntarget();
    }
    private void Respawntarget()
    {
        // 0�`List�̌��͈̔͂Ń����_���Ȑ����l���Ԃ�
        _spawnPosition = _positionList[Random.Range(0, _positionList.Count)].transform.position;
        target = Instantiate(_spawnObject[Random.Range(0, _spawnObject.Count)],
                    _spawnPosition,
                    Quaternion.Euler(0, 0, 0));

        

    }
}
