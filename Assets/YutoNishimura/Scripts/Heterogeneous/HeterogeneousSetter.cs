using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<GameObject> objSpawnPos;
    private int rand;                    //�����_���Ȑ���
    private int fieldObjectsNum;         //�t�B�[���h���ɂ���َ��Ȃ��̂̌�

    void Start()
    {
        fieldObjectsNum = 0;
        objSpawnPos = new List<GameObject>();
        queue = new Queue<GameObject>();
        queue.Enqueue(ObjectA);
        queue.Enqueue(ObjectB);
        queue.Enqueue(ObjectC);
        queue.Enqueue(ObjectD);

        //GameObject�^���X�g��8�ӏ��S�Ăɂ�null�����ď���������
        for (int i = 0; i < points.Count(); i++)
        {
            objSpawnPos.Add(null);
        }

        InitialSetObjects();
    }

    void Update()
    {
        SetObjects();
    }

    //�Q�[���J�n���W��������|�C���g�Ƀ����_���ɂR�����I�u�W�F�N�g��z�u����
    private void InitialSetObjects()
    {
        for(int i = 0; i < 3; i++)
        {
            int rnd = Random.Range(0, 8);

            while(rand == rnd)
            {
                rnd = Random.Range(0, 8);
            }

            objSpawnPos[rnd] = Instantiate(GetNextObject(), points[rnd].transform.position, Quaternion.identity);
        }
    }

    private void SetObjects()
    {
        for(int i = 0; i < points.Count(); i++)
        {
            if (objSpawnPos[i] != null)
            {
                fieldObjectsNum++;
            }
            else
            {
                if(fieldObjectsNum >= 3)
                {
                    return;
                }

                objSpawnPos[i] = Instantiate(GetNextObject(), points[i].transform.position, Quaternion.identity);
            }
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