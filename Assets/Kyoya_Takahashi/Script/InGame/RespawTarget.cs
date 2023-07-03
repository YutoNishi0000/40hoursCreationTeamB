using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawTarget : MonoBehaviour
{
    //���[�g�̎�ށi�S4��ށj
    public enum RootType
    {
        First,
        Second,
        Third,
        Fourth
    }

    [SerializeField] private GameObject targetObj;        //�^�[�Q�b�g�I�u�W�F�N�g

    [SerializeField] private GameObject root1Pos;
    [SerializeField] private GameObject root2Pos;
    [SerializeField] private GameObject root3Pos;
    [SerializeField] private GameObject root4Pos;

    public static GameObject tempTarget;             //�^�[�Q�b�g�̃I�u�W�F�N�g��ێ����邽�߂̕ϐ�

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
        //1�t���[���҂i���X�|�[���̎��Ƀ|�X�g�G�t�F�N�g�������邽�߁j
        await UniTask.DelayFrame(1);

        int rootType = Random.Range(0, 4);

        SEManager.Instance.PlayRespawn();

        //���[�g�^�C�v�ɂ���ăX�|�[������ʒu���ς��
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

    //�t�B�[���h��ɑ��݂��Ă���^�[�Q�b�g�I�u�W�F�N�g���擾
    public static GameObject GetCurrentTargetObj() { return tempTarget; }
}
