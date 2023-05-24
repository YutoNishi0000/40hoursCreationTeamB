using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawTarget : MonoBehaviour
{
    //���[�g�̎��
    private enum Root
    {
        First,
        Second,
        Third,
        Fourth
    }

    [Header("�^�[�Q�b�g�I�u�W�F�N�g")]
    [SerializeField] private GameObject targetObj;
    [Header("�^�[�Q�b�g�̃��X�|�[���ʒu�i���[�g�̏��Ԃɍ��킹�Ă��������j")]
    [SerializeField] private List<GameObject> targetSpawnPos;
    [Header("�^�[�Q�b�g��ꃋ�[�g")]
    [SerializeField] private List<GameObject> root1;
    [Header("�^�[�Q�b�g��񃋁[�g")]
    [SerializeField] private List<GameObject> root2;
    [Header("�^�[�Q�b�g��O���[�g")]
    [SerializeField] private List<GameObject> root3;
    [Header("�^�[�Q�b�g��l���[�g")]
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
    //    //�^�[�Q�b�g�����݂��Ă���^�[�Q�b�g����
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

    //�^�[�Q�b�g�����X�|�[���A�^�[�Q�b�g�̃��[�g��ݒ肷��֐�
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