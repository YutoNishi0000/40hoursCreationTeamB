using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//�X�L���Ɋւ���֐�

//JudgeScore�R���|�[�l���g�𓮓I�ɒǉ������� <- �Q�[���V�[���Ɉړ������Ƃ�
public class SkillManager : Actor
{
    private bool addScoreFlag;         //�X�R�A���Z�t���O
    private bool targetMinimapFlag;     //�^�[�Q�b�g�̃~�j�}�b�v��\�����邩�ǂ���
    private bool skillBlock_player;
    private bool skillBlock_addScore;
    private bool skillBlock_seeTarget;
    private float time;
    [SerializeField] private readonly float interval = 5.0f;
    private int shutterTimeStamp;
    private bool minimapSkillFlag;         //�^�[�Q�b�g�̃~�j�}�b�v�̕\���t���O�̂��߂Ɏg��
    private const int skillLevel1 = 5;
    private const int skillLevel2 = 10;
    private const int skillLevel3 = 20;
    private float playerAccelSpeed;
    private readonly float accelerationSpeed = 1.5f;   //�v���C���[�̃X�L���l�����̑��x�{��
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
        playerAccelSpeed = player.GetInitialPlayerSpeed() * accelerationSpeed;
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
        switch (GameManager.Instance.numSubShutter)
        {
            case skillLevel1:
                if (skillBlock_player)
                {
                    SEManager.Instance.PlaySkill();
                    skillBlock_player = false;
                }
                break;
            case skillLevel2:
                if (skillBlock_addScore)
                {
                    SEManager.Instance.PlaySkill();
                    skillBlock_addScore = false;
                }
                break;
            case skillLevel3:
                if (skillBlock_seeTarget)
                {
                    SEManager.Instance.PlaySkill();
                    skillBlock_seeTarget = false;
                }
                break;
            default:
                break;
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
        //Debug.Log("����");
        PlayerSpeedUp();

        if (skillBlock_addScore)
        {
            return;
        }

        AddScore();

        if(skillBlock_seeTarget)
        {
            return;
        }

        TargetMinimapActivation();
    }

    /// <summary>
    /// �X�R�A���Z
    /// </summary>
    private void AddScore()
    {
        SetAddScoreFlag(true);
    }

    /// <summary>
    /// �^�[�Q�b�g�̃~�j�}�b�v��\������
    /// </summary>
    private void TargetMinimapActivation()
    {
        int count = (GameManager.Instance.numSubShutter - skillLevel3) / minimapTargetShutterNum;

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
        playerInstance.SetPlayerSpeed(9);
    }

    //�^�[�Q�b�g�̃~�j�}�b�v��\�����邩�ǂ���
    public void SetTargetMinimapFlag(bool flag) { targetMinimapFlag = flag; }

    public bool GetTargetMinimapFlag() { return targetMinimapFlag; }

    //�X�R�A���Z�t���O
    public void SetAddScoreFlag(bool flag) { addScoreFlag = flag; }

    public bool GetAddScoreFlag() { return addScoreFlag; }

    // �X�s�[�h
    public bool GetPlayerSpeedFlag() { return skillBlock_player; }

}
