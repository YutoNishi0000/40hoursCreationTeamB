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
    [SerializeField] private GameObject[] parentWonderPoints;       //���[�g�|�C���g�̐e�I�u�W�F�N�g�����Ă��������i�����َ��Ȃ��́j
    private Queue<GameObject> queue;
    public List<GameObject> objSpawnPos;
    private List<int> pos;
    private int rand;                    //�����_���Ȑ���
    private int fieldObjectsNum;         //�t�B�[���h���ɂ���َ��Ȃ��̂̌�
    private ScreenShot screen;
    private readonly int numStrangeObjInField = 8;
    private readonly static float respawnCoolTime = 10;    //�َ��Ȃ��̂��Đ��������܂ł̃N�[���^�C��
    private static float coolTime;

    void Start()
    {
        coolTime = 0;
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

    void LateUpdate()
    {
        //Debug.Log("1isitunamono" + objSpawnPos.Count);
        SetObjects();
        screen.SetList(objSpawnPos);
    }

    //�Q�[���J�n���W��������|�C���g�Ƀ����_���ɂR�����I�u�W�F�N�g��z�u����
    private void InitialSetObjects()
    {
        List<int> rnd = new List<int>();

        for (int i = 0; i < numStrangeObjInField; i++)
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

    public void SetObjects()
    {
        coolTime -= Time.deltaTime;

        if(coolTime > 0)
        {
            return;
        }
        else
        {
            coolTime = 0;
        }

        //���َ��Ȃ��̂����z�u����Ă���̂����m�F
        for(int i = 0; i < points.Count(); i++)
        {
            if (objSpawnPos[i] != null)
            {
                fieldObjectsNum++;
            }
        }

        Debug.Log("���t�B�[���h�Ɉَ��Ȃ��̂�" + fieldObjectsNum + "�q���݂��Ă��܂�");

        //����Ȃ�����₤�悤�Ȍ`�ňَ��Ȃ��̂𓮓I�ɔz�u����
        for (int j = 0; j < numStrangeObjInField - fieldObjectsNum; j++)
        {
            int rand = Random.Range(0, points.Count());

            while (objSpawnPos[rand] != null)
            {
                rand = Random.Range(0, points.Count());
            }

            objSpawnPos[rand] = Instantiate(GetNextObject(), points[rand].transform.position, GetNextObject().transform.rotation);

            //MetalonController��null�ł͂Ȃ�������
            if (objSpawnPos[rand].GetComponent<MetalonController>())
            {
                objSpawnPos[rand].GetComponent<MetalonController>().SetRootType(rand, parentWonderPoints);
            }
        }

        fieldObjectsNum = 0;
    }

    public static void CoolTime()
    {
        coolTime = respawnCoolTime;
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