using Cysharp.Threading.Tasks;
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
    [SerializeField] private List<GameObject> parentWonderPoints;       //���[�g�|�C���g�̐e�I�u�W�F�N�g�����Ă��������i�����َ��Ȃ��́j
    private Queue<GameObject> queue;
    public List<GameObject> objSpawnPos;
    private List<int> pos;
    private int rand;                    //�����_���Ȑ���
    private int fieldObjectsNum;         //�t�B�[���h���ɂ���َ��Ȃ��̂̌�
    private ScreenShot screen;
    private readonly int numStrangeObjInField = Config.numSubTargetInField;
    private readonly static float respawnCoolTime = Config.subSubTargetGenerationCoolTime;    //�َ��Ȃ��̂��Đ��������܂ł̃N�[���^�C��
    private static float coolTime;
    public static int numRand;
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

        //InitialSetObjects();
    }

    void LateUpdate()
    {
        SetObjects();
        screen.SetList(objSpawnPos);
    }

    //�Q�[���J�n���W��������|�C���g�Ƀ����_���ɂR�����I�u�W�F�N�g��z�u����
    private void InitialSetObjects()
    {
        List<int> rnd = new List<int>();

        //�ŏ��͎w�肷�镪�����َ��Ȃ��̂𐶐�
        for (int i = 0; i < numStrangeObjInField; i++)
        {
            rnd.Add(Random.Range(0, points.Count()));

            //�������A�����_���Ɏ擾�����l�����ɏo�Ă��܂��Ă�����Ⴄ�l���o��܂Ŏ擾��������
            for(int j = 0; j < rnd.Count(); j++)
            {
                while(rnd[i] == rnd[j] && i != j)
                {
                    rnd[i] = Random.Range(0, points.Count());
                }
            }

            //Instantiate��Template��Ԃ��̂𗘗p����bool�^���擾
            objSpawnPos[rnd[i]] = Instantiate(GetNextObject(), points[rnd[i]].transform.position, Quaternion.identity);

            //�����َ��Ȃ��̂ɂ͐����ꏊ�̃A�h���X��ݒ�i�ڍׂ�MetalonController.cs�ցj
            if (objSpawnPos[rnd[i]].GetComponentInChildren<MetalonController>())
            {
                SetMetalonConfig(rnd[i], parentWonderPoints);
            }
        }
    }

    //���Ԃ�Ȃ��悤�ɋ󂢂Ă���َ��Ȃ��̂̐����|�C���g�ɐ�������
    public void SetObjects()
    {
        //�N�[���^�C����ݒu
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

        //����Ȃ�����₤�悤�Ȍ`�ňَ��Ȃ��̂𓮓I�ɔz�u����
        for (int j = 0; j < numStrangeObjInField - fieldObjectsNum; j++)
        {
            //���Ȃ��ꏊ�ɐ������Ȃ��悤�ɂ��Ԃ�Ȃ������_���Ȓl���擾
            int rand = Random.Range(0, points.Count());

            while (objSpawnPos[rand] != null)
            {
                rand = Random.Range(0, points.Count());
            }

            //Instantiate��Template��Ԃ��̂𗘗p����GameObject�^���擾
            objSpawnPos[rand] = Instantiate(GetNextObject(), points[rand].transform.position, GetNextObject().transform.rotation);

            //�����َ��Ȃ��̂ɂ͐����ꏊ�̃A�h���X��ݒ�i�ڍׂ�MetalonController.cs�ցj
            if (objSpawnPos[rand].GetComponent<MetalonController>())
            {
                SetMetalonConfig(rand, parentWonderPoints);
            }
        }

        fieldObjectsNum = 0;
    }

    private void SetMetalonConfig(int num, List<GameObject> parentPoints)
    {
        Debug.Log("�Z�b�g");
        MetalonController metaron = objSpawnPos[num].GetComponent<MetalonController>();
        //���[�g��񂪓������e�I�u�W�F�N�g���Z�b�g
        metaron.SetWonderParentPoints(parentPoints);
        //�X�|�[���i���o�[���Z�b�g
        metaron.SetSpawnNumber(num);
    }

    //�N�[���^�C������
    public static void CoolTime()
    {
        coolTime = respawnCoolTime;
    }

    //�L���[���g�p���Ď��̗v�f�̃I�u�W�F�N�g���L���[�ɃZ�b�g�A�擾����֐�
    private GameObject GetNextObject()
    {
        if (queue.Count == 0)
        {
            return null;
        }

        //�v�f���������Ɏ擾
        GameObject obj  = queue.Peek();

        //�v�f�����o��
        queue.Enqueue(obj);

        //�擾�����v�f�͔p��
        return queue.Dequeue();
    }

    public ref List<GameObject> GetObjSpawnPos() { return ref objSpawnPos; }
}