using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//�X�L���Ɋւ���֐�

//JudgeScore�R���|�[�l���g�𓮓I�ɒǉ������� <- �Q�[���V�[���Ɉړ������Ƃ�
public class SkillManager : Actor
{
    private static bool addScoreFlag;         //�X�R�A���Z�t���O
    private bool targetMinimapFlag;     //�^�[�Q�b�g�̃~�j�}�b�v��\�����邩�ǂ���
    private bool skillBlock_player;
    private bool skillBlock_addScore;
    private bool skillBlock_seeTarget;
    private float time;
    [SerializeField] private readonly float interval = Config.intervalActiveTargetMInimap;
    private int shutterTimeStamp;
    private bool minimapSkillFlag;         //�^�[�Q�b�g�̃~�j�}�b�v�̕\���t���O�̂��߂Ɏg��
    private float playerAccelSpeed;
    private readonly float accelerationSpeed = Config.raisePlayerSpeed;   //�v���C���[�̃X�L���l�����̑��x�{��
    private readonly int minimapTargetShutterNum = 5;   //���������ɃX�L�����������邩
    private int previousCount;
    private Player player;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        time = interval;
        targetMinimapFlag = false;
        skillBlock_player = true;
        skillBlock_addScore = true;
        skillBlock_seeTarget = true;
        addScoreFlag = false;
        minimapSkillFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(skillBlock_player);
        UnLockSkill();
        SkillImposition();
    }

    /// <summary>
    /// �X�L�����b�N����
    /// </summary>
    private void UnLockSkill()
    {
        if(GameManager.Instance.numSubShutter == Config.skillLevel1 && skillBlock_player)
        {
            SEManager.Instance.PlaySkill();
            skillBlock_player = false;
        }
        else if (GameManager.Instance.numSubShutter == Config.skillLevel2 && skillBlock_addScore)
        {
            SEManager.Instance.PlaySkill();
            skillBlock_addScore = false;
        }
        else if (GameManager.Instance.numSubShutter == Config.skillLevel3 && skillBlock_seeTarget)
        {
            SEManager.Instance.PlaySkill();
            skillBlock_seeTarget = false;
        }
    }

    /// <summary>
    /// �X�L�������֐�
    /// </summary>
    public void SkillImposition()
    {
        if(skillBlock_player)
        {
            return;
        }

        //�v���C���[���x�A�b�v�J��
        PlayerSpeedUp();

        if (skillBlock_addScore)
        {
            return;
        }

        //�X�R�A�A�b�v�J��
        SetSpiritSenceFlag(true);

        if (skillBlock_seeTarget)
        {
            return;
        }

        //�^�[�Q�b�g�������
        TargetMinimapActivation();
    }

    /// <summary>
    /// �X�R�A���Z
    /// </summary>
    private void AddScore()
    {
        SetSpiritSenceFlag(true);
    }

    /// <summary>
    /// �^�[�Q�b�g�̃~�j�}�b�v��\������
    /// </summary>
    private void TargetMinimapActivation()
    {
        int count = (GameManager.Instance.numSubShutter - Config.skillLevel3) / minimapTargetShutterNum;

        time -= Time.deltaTime;

        if(time > 0)
        {
            SetTargetMinimapFlag(true);
        }
        else
        {
            SetTargetMinimapFlag(false);
            time = 0;
        }

        if (count == previousCount + 1)
        {
            time = interval;
        }

        previousCount = count;
    }

    /// <summary>
    /// �v���C���[�̈ړ����x�A�b�v
    /// </summary>
    private void PlayerSpeedUp()
    {
        playerAccelSpeed = player.GetInitialPlayerSpeed() * accelerationSpeed;
        playerInstance.SetPlayerSpeed(playerAccelSpeed);
    }

    //�^�[�Q�b�g�̃~�j�}�b�v��\�����邩�ǂ���
    public void SetTargetMinimapFlag(bool flag) { targetMinimapFlag = flag; }

    public bool GetTargetMinimapFlag() { return targetMinimapFlag; }

    //�X�R�A���Z�t���O
    public static void SetSpiritSenceFlag(bool flag) { addScoreFlag = flag; }

    public static bool GetSpiritSenceFlag() { return addScoreFlag; }

    // �X�s�[�h
    public bool GetPlayerSpeedFlag() { return skillBlock_player; }

}
