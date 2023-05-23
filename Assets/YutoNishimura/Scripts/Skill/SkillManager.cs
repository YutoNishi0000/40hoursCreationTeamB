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
    private const int skillLevel1 = 15;
    private const int skillLevel2 = 26;
    private const int skillLevel3 = 42;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
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
        switch(GameManager.Instance.numSubShutter)
        {
            case skillLevel1:
                skillBlock_player = false;
                GameManager.Instance.numSubShutter++;
                break;
            case skillLevel2:
                skillBlock_addScore = false;
                GameManager.Instance.numSubShutter++;
                break;
            case skillLevel3:
                skillBlock_seeTarget = false;
                //GameManager.Instance.numSubShutter++;
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
        if(!skillBlock_player)
        {
            Debug.Log("����");
            PlayerSpeedUp();
            skillBlock_player = true;
        }

        if(skillBlock_addScore)
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
        int count = GameManager.Instance.numSubShutter - 40;

        if(count % 10 == 0 && !minimapSkillFlag)
        {
            minimapSkillFlag = true;
        }

        if(!minimapSkillFlag)
        {
            return;
        }

        time += Time.deltaTime;

        if(time <= interval)
        {
            SetTargetMinimapFlag(true);
        }
        else if((time > interval) && (time <= interval + 10))
        {
            SetTargetMinimapFlag(false);
        }
        else
        {
            minimapSkillFlag = false; 
        }
    }

    /// <summary>
    /// �v���C���[�̈ړ����x�A�b�v
    /// </summary>
    private void PlayerSpeedUp()
    {
        playerInstance.SetPlayerSpeed(playerInstance.GetPlayerSpeed() * 1.5f);
    }

    //�^�[�Q�b�g�̃~�j�}�b�v��\�����邩�ǂ���
    public void SetTargetMinimapFlag(bool flag) { targetMinimapFlag = flag; }

    public bool GetTargetMinimapFlag() { return targetMinimapFlag; }

    //�X�R�A���Z�t���O
    public void SetAddScoreFlag(bool flag) { addScoreFlag = flag; }

    public bool GetAddScoreFlag() { return addScoreFlag; }

}
