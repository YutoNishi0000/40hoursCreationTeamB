using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeScore : ScoreManger
{
    //�r���[���W�ɕϊ��������I�u�W�F�N�g�|�W�V����
    [SerializeField] private GameObject obj = null;

    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject player = null;
    //���ꂼ��̃X�R�A�̒l
    public enum ScoreType
    {
        high = 50,
        midle = 30,
        low = 10,
        outOfScreen = 0,
    };

    //�X�R�A�{��(���x���ɉ����Ēl���قȂ�)
    private readonly float Odds_Level1 = 1.2f;
    private readonly float Odds_Level2 = 1.5f;
    private readonly float Odds_Level3 = 2.0f;

    //���ꂼ��̔���̕�
    private const float areaWidth = 192;      //(960 / 5) ���5����
    private const float areaHeight = 216;     //(1080 / 5) ���5����

    //��ʂ̒��S
    static Vector3 center = Vector3.zero;

    //�J�����̃N�[���^�C��
    private float coolTime = 3;
    //�J�����g�p�\��
    private bool cameraEnable = true;
    private const float raiseScore = 1.5f;     //�X�R�A�㏸�{��
    private ScreenShot screen;
    [SerializeField] private Image getTimeImg;

    private void Start()
    {
        getTimeImg.enabled = false;
        //��ʂ̒��S�����߂�
        center = new Vector3(
            areaWidth * 2 + areaWidth * 0.5f,
            areaHeight * 2 + areaHeight * 0.5f,
            0.0f);

        screen = GameObject.FindObjectOfType<ScreenShot>();
    }
    private void LateUpdate()
    {
        if (Shutter.isFilming)
        {
            Shutter.isFilming = false;
            //Debug.Log("�ʂ��Ă�(1)");
            //��Q��������Ƃ�
            if (createRay())
            {
                Debug.Log("��Q�a");
                SEManager.Instance.PlayShot();
                return;
            }
            //�^�[�Q�b�g����ʊO��
            if (checkScore(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position)) == (int)ScoreType.outOfScreen)
            {
                SEManager.Instance.PlayShot();
                Debug.Log("��ʊO");
                return;
            }

            //���Ԃ��l��
            CountDownTimer.IncreaceTime();
            Debug.Log("���Ԃ��l�����܂���");
            //�^�[�Q�b�g���B�e���ꂽ
            screen.SetPhotographTargetFlag(false);
            //��Q�����Ȃ��Ƃ��̏���
            Debug.Log("�B�e����");
            //�X�R�A���Z
            ScoreManger.Score += checkScore(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position));
            ScoreManger.ShotMainTarget = true;
            TargetManager.IsSpawn = true;
            //�Ώۂ��B�e�����񐔂��C���N�������g
            GameManager.Instance.numTargetShutter++;
            //SE
            SEManager.Instance.PlayTargetShot();
            //�^�[�Q�b�g���X�|�[��
            RespawTarget.RespawnTarget();
            //�A�j���[�V�����J�n
            Invoke("startTA", 0.2f);

            //screen.FadeIn(getTimeImg);
        }
    }
    /// <summary>
    /// ���[���h���W���X�N���[�����W��
    /// </summary>
    /// <param name="cam">�J�����I�u�W�F�N�g</param>
    /// <param name="worldPosition">���[���h���W</param>
    /// <returns>�X�N���[�����W</returns>
    private Vector3 WorldToScreenPoint(Camera cam, Vector3 worldPosition)
    {
        // �J������Ԃɕϊ�(�J�������猩�����W�ɕϊ�)
        Vector3 cameraSpace = cam.worldToCameraMatrix.MultiplyPoint(worldPosition);

        // �N���b�s���O��Ԃɕϊ�(cameraSpace�����͈̔͂ɍi���Ă�)
        Vector4 clipSpace = cam.projectionMatrix * new Vector4(cameraSpace.x, cameraSpace.y, cameraSpace.z, 1f);

        //�f�o�C�X���W�F�����[1�@�E��{1
        //�����Ă�̂͐��K��
        // �f�o�C�X���W�n�ɕϊ�
        Vector3 deviceSpace = new Vector3(clipSpace.x / clipSpace.w, clipSpace.y / clipSpace.w, clipSpace.z / clipSpace.w);

        // �X�N���[�����W�n�ɕϊ�
        Vector3 screenSpace = new Vector3((deviceSpace.x + 1f) * 0.25f * Screen.width, (deviceSpace.y + 1f) * 0.5f * Screen.height, deviceSpace.z);

        return screenSpace;
    }
    /// <summary>
    /// �����ɂǂꂾ���߂�������
    /// </summary>
    /// <param name="scrPoint">�X�N���[�����W</param>
    /// <returns>�X�R�A�̒l</returns>
    private float checkScore(Vector3 scrPoint)
    {
        //return lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));
        int defaultScore = lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));

        //�X�L���ɂ��X�R�A���Z�t���O���I����������if���̏��������s
        if (GameManager.Instance.skillManager.GetAddScoreFlag())
        {
            //�t���O���I����������1.5�{�̃X�R�A��Ԃ�
            return defaultScore * raiseScore;
        }
        else
        {
            return defaultScore;
        }
    }
    /// <summary>
    /// �X�R�A�̔���(��)
    /// </summary>
    /// <param name="scrPoint">�X�N���[�����W</param>
    /// <param name="score">�^�e�����Ō����Ƃ��̃X�R�A</param>
    /// <returns>�X�R�A</returns>
    private int checkScoreHori(Vector3 scrPoint)
    {
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2)
        {
            return (int)ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth)
        {
            return (int)ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth * 2)
        {
            return (int)ScoreType.low;
        }
        return (int)ScoreType.outOfScreen;
    }
    /// <summary>
    /// �X�R�A�̔���(�c)
    /// </summary>
    /// <param name="scrPoint">�X�N���[�����W</param>
    /// <param name="score">�^�e�����Ō����Ƃ��̃X�R�A</param>
    /// <returns>�X�R�A</returns>
    private int checkScoreVart(Vector3 scrPoint)
    {
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2)
        {
            return (int)ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2 + areaHeight)
        {
            return (int)ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2 + areaHeight * 2)
        {
            return (int)ScoreType.low;
        }
        return (int)ScoreType.outOfScreen;
    }
    /// <summary>
    /// �Ⴂ�ق��̒l�������߂�
    /// </summary>
    /// <param name="v">�Е��̃X�R�A</param>
    /// <returns>�X�R�A</returns>
    private int lower(int v1, int v2)
    {
        if (v1 < v2)
        {
            return v1;
        }
        else
        {
            return v2;
        }
    }
    RaycastHit hit;
    private bool createRay()
    {
        Vector3 diff = RespawTarget.GetCurrentTargetObj().transform.position - player.transform.position;

        Vector3 direction = diff.normalized;
        //Debug.Log(direction);
        float distance = Vector3.Distance(RespawTarget.GetCurrentTargetObj().transform.position, player.transform.position);
        Debug.DrawRay(player.transform.position, distance * direction, Color.green, 1f);
        if (Physics.Raycast(player.transform.position, direction, out hit, distance))
        {
            //Physics.Raycast(player.transform.position, direction, out hit, distance);
           // spere.transform.position = hit.point;
            Debug.Log("�X�e�[�W�ɓ������Ă�");
            return true;
        }
        Debug.Log("�X�e�[�W�ɓ������ĂȂ�");
        return false;     
        
    }
    void startTA()
    {
        ShutterAnimation.TargetAnimationStart();
    }
}

