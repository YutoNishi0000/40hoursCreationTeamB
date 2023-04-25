using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�َ��Ȃ���
public class HeterogeneousSetter : MonoBehaviour
{
    [SerializeField] private GameObject[] points;   //�����_�ł�8�ӏ��Ɖ���
    [SerializeField] private GameObject ObjectA;
    [SerializeField] private GameObject ObjectB;
    [SerializeField] private GameObject ObjectC;
    [SerializeField] private GameObject ObjectD;
    private Queue<GameObject> queue;
    private int rand;                    //�����_���Ȑ���

    void Start()
    {
        queue = new Queue<GameObject>();
        queue.Enqueue(ObjectA);
        queue.Enqueue(ObjectB);
        queue.Enqueue(ObjectC);
        queue.Enqueue(ObjectD);

        SetObjects();
    }

    void Update()
    {

    }

    //�Q�[���J�n���W��������|�C���g�Ƀ����_���ɂR�����I�u�W�F�N�g��z�u����
    private void SetObjects()
    {
        for(int i = 0; i < 3; i++)
        {
            int rnd = Random.Range(0, 8);

            while(rand == rnd)
            {
                rnd = Random.Range(0, 8);
            }

            Instantiate(GetNextObject(), points[rnd].transform.position, Quaternion.identity);
        }
    }

    //�L���[���g�p���Ď��̃I�u�W�F�N�g���L���[�ɃZ�b�g�A�擾����֐�
    private GameObject GetNextObject()
    {
        if (queue.Count == 0)
        {
            return null;
        }

        GameObject obj  = queue.Peek();

        queue.Enqueue(obj);

        return queue.Dequeue();
    }
}