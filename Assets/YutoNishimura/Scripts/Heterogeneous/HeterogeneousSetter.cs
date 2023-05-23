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
    public List<GameObject> objSpawnPos;   //Don't Touch!!
    private List<int> pos;
    private int rand;                    //�����_���Ȑ���
    private int fieldObjectsNum;         //�t�B�[���h���ɂ���َ��Ȃ��̂̌�
    private ScreenShot screen;

    void Start()
    {
        screen = GameObject.FindObjectOfType<ScreenShot>();
        fieldObjectsNum = 0;
        objSpawnPos = new List<GameObject>();
        queue = new Queue<GameObject>();
        pos = new List<int>();
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
        Debug.Log("1isitunamono" + objSpawnPos.Count);
        SetObjects();
        screen.SetList(objSpawnPos);
    }

    //�Q�[���J�n���W��������|�C���g�Ƀ����_���ɂR�����I�u�W�F�N�g��z�u����
    private void InitialSetObjects()
    {
        List<int> rnd = new List<int>();

        for (int i = 0; i < 8; i++)
        {
            rnd.Add(Random.Range(0, points.Count()));

            for(int j = 0; j < rnd.Count(); j++)
            {
                while(rnd[i] == rnd[j] && i != j)
                {
                    rnd[i] = Random.Range(0, points.Count());
                }
            }

            objSpawnPos[rnd[i]] = Instantiate(GetNextObject(), points[rnd[i]].transform.position, Quaternion.identity);
        }
    }

    private void SetObjects()
    {
        //���َ��Ȃ��̂����z�u����Ă���̂����m�F
        for(int i = 0; i < points.Count(); i++)
        {
            if (objSpawnPos[i] != null)
            {
                fieldObjectsNum++;
            }
        }

        //�������َ��Ȃ��̂̃f�[�^�����S�ɍ폜���ꂽ�炱�����牺�̏������ĊJ����
        for (int i = 0; i < screen.GetDestroyList().Count; i++)
        {
            //���S�ɏ����Ă��Ȃ�������return
            if (objSpawnPos[screen.GetDestroyList()[i]] != null)
            {
                return;
            }
        }

        //�����ɂ��Ă���Ƃ������Ƃ́A���S�ɎB�e���ꂽ�َ��Ȃ��̂��폜���ꂽ���ƂɂȂ�̂Ń��X�g������������
        screen.SetDestroyList(null);

        //����Ȃ�����₤�悤�Ȍ`�ňَ��Ȃ��̂𓮓I�ɔz�u����
        for (int j = 0; j < 8 - fieldObjectsNum; j++)
        {
            int rand = Random.Range(0, points.Count());

            while (objSpawnPos[rand] != null)
            {
                rand = Random.Range(0, points.Count());
            }

            objSpawnPos[rand] = Instantiate(GetNextObject(), points[rand].transform.position, Quaternion.identity);
        }

        fieldObjectsNum = 0;
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

    public ref List<GameObject> GetObjSpawnPos() { return ref objSpawnPos; }
}