using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//�X�L���Ɋւ���֐�

//�����F�X�R�A���Z�͂��傤��ɔC������

//JudgeScore�R���|�[�l���g�𓮓I�ɒǉ������� <- �Q�[���V�[���Ɉړ������Ƃ�
public class SkillManager : Actor
{
    private enum SkillType
    {
        AddScore,      //�X�R�A���Z
        SpeedUp,       //�v���C���[�̈ړ��X�s�[�h�A�b�v
        SpeedDown,     //�Ώۂ̈ړ��X�s�[�h�_�E��
        SeeTargetPos   //�Ώۂ̈ʒu�������~�j�}�b�v�\��
    }

    private enum SkillLevel
    {
        Level1,
        Level2,
        Level3,
        Failed
    }

    private JudgeScore judgeScore;
    private SkillType skillType;
    private SkillLevel skillLevel;
    private bool countflag;           //�X�L���擾�J�E���g�t���O
    private bool targetMinimapFlag;     //�^�[�Q�b�g�̃~�j�}�b�v��\�����邩�ǂ���
    private readonly float level1_buf = 1.2f;
    private readonly float level2_buf = 1.5f;
    private readonly float level3_buf = 2.0f;
    private readonly float level1_debuf = 0.8f;
    private readonly float level2_debuf = 0.6f;
    private readonly float level3_debuf = 0.5f;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        judgeScore = GetComponent<JudgeScore>();
        skillType = new SkillType();
        skillLevel = new SkillLevel();
        countflag = false;
        targetMinimapFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(countflag)
        {
            time += Time.deltaTime;
        }
    }

    /// <summary>
    /// �X�L���J�E���g�J�n�����̊֐���K���Ăяo��
    /// </summary>
    public void StartCount()
    {
        countflag = true;
    }

    public void EndCount()
    {
        countflag = false;

        //time = 0;
    }

    /// <summary>
    /// �X�L�������֐��i�P�t���[�������Ăяo���j
    /// </summary>
    public void SkillImposition()
    {
        switch(GetSkillType())
        {
            case SkillType.AddScore:
                AddScore();
                SkillReset();     //�O�̂��߃X�L�����Z�b�g���s���Ă���
                break;
            case SkillType.SpeedUp:
                PlayerSpeedUp();
                //10�b��ɃX�L�����Z�b�g
                Invoke(nameof(SkillReset), 10);
                break;
            case SkillType.SpeedDown:
                TargetSpeedDown();
                //5�b��ɃX�L�����Z�b�g
                Invoke(nameof(SkillReset), 5);
                break;
            case SkillType.SeeTargetPos:
                SetTargetMinimapFlag(true);
                //5�b��ɃX�L�����Z�b�g
                Invoke(nameof(SkillReset), 5);
                break;
        }

        //�^�C�}�[�J�E���g���I��
        EndCount();
    }

    /// <summary>
    /// �����FInvoke�ŌĂяo��
    /// �X�L�����d�����Ȃ��悤�ɑS�ẴX�L�������Z�b�g����
    /// </summary>
    public void SkillReset()
    {
        playerInstance.SetPlayerSpeed(playerInstance.GetInitialPlayerSpeed());
        targetInstance.SetTargetSpeed(targetInstance.GetInitialTargetSpeed());
        SetTargetMinimapFlag(false);
    }

    /// <summary>
    /// �X�L���̎�ނ��m���ɉ����Ď擾
    /// </summary>
    /// <returns></returns>
    private SkillType GetSkillType()
    {
        //Random.value��0����1�͈̔͂̒l��Ԃ�
        float randValue = Random.value * 100;

        if(randValue >= 0 && randValue < 30) 
        {
            return SkillType.AddScore;
        }
        else if(randValue >= 30 && randValue < 60)
        {
            return SkillType.SpeedUp;
        }
        else if (randValue >= 60 && randValue < 90)
        {
            return SkillType.SpeedDown;
        }
        else
        {
            return SkillType.SeeTargetPos;
        }
    }

    /// <summary>
    /// �X�R�A�֌W�������肽��
    /// </summary>
    private void AddScore()
    {
        switch (GetSkillLevel(ref time))
        {
            case SkillLevel.Level1:
                judgeScore.SetOddsType(JudgeScore.OddsType.LEVEL1);
                break;
            case SkillLevel.Level2:
                judgeScore.SetOddsType(JudgeScore.OddsType.LEVEL2);
                break;
            case SkillLevel.Level3:
                judgeScore.SetOddsType(JudgeScore.OddsType.LEVEL3);
                break;
        }
    }

    private void TargetSpeedDown()
    {
        float target_speed = targetInstance.GetTargetSpeed();

        switch (GetSkillLevel(ref time))
        {
            case SkillLevel.Level1:
                playerInstance.SetPlayerSpeed(target_speed * level1_debuf);
                break;
            case SkillLevel.Level2:
                playerInstance.SetPlayerSpeed(target_speed * level2_debuf);
                break;
            case SkillLevel.Level3:
                playerInstance.SetPlayerSpeed(target_speed * level3_debuf);
                break;
        }
    }

    private void PlayerSpeedUp()
    {
        float player_speed = playerInstance.GetPlayerSpeed();

        switch(GetSkillLevel(ref time))
        {
            case SkillLevel.Level1:
                playerInstance.SetPlayerSpeed(player_speed * level1_buf);
                break;
            case SkillLevel.Level2:
                playerInstance.SetPlayerSpeed(player_speed * level2_buf);
                break;
            case SkillLevel.Level3:
                playerInstance.SetPlayerSpeed(player_speed * level3_buf);
                break;
        }
    }

    public bool GetTargetMinimapFlag() { return targetMinimapFlag; }

    public void SetTargetMinimapFlag(bool flag) { targetMinimapFlag = flag; }

    /// <summary>
    /// �X�L�����x�����擾
    /// </summary>
    /// <returns>�A�h���X</returns>
    /// time�͂��̃N���X���Ŏg���܂킵�����[������������팸�̂���
    private SkillLevel GetSkillLevel(ref float resultTime)
    {
        //�Q�Ɠn�������ē���ꂽ�����̃A�h���X�𓮓I�ɃR�s�[
        float s_time = resultTime;

        //�A�h���X�̒��̒l��������
        resultTime = 0;

        switch(GameManager.Instance.GetGameMode())
        {
            case GameManager.GameMode.Easy:

                if(s_time >= 0 && s_time < 30)
                {
                    return SkillLevel.Level3;
                }
                else if(s_time >= 30 && s_time < 150)
                {
                    return SkillLevel.Level2;
                }
                else if(s_time >= 150 && s_time < 300)
                {
                    return SkillLevel.Level1;
                }
                else
                {
                    return SkillLevel.Failed;
                }

            case GameManager.GameMode.Nomal:

                if (s_time >= 0 && s_time < 90)
                {
                    return SkillLevel.Level3;
                }
                else if (s_time >= 90 && s_time < 270)
                {
                    return SkillLevel.Level2;
                }
                else if (s_time >= 270 && s_time < 420)
                {
                    return SkillLevel.Level1;
                }
                else
                {
                    return SkillLevel.Failed;
                }

            case GameManager.GameMode.Hard:

                if (s_time >= 0 && s_time < 90)
                {
                    return SkillLevel.Level3;
                }
                else if (s_time >= 90 && s_time < 360)
                {
                    return SkillLevel.Level2;
                }
                else if (s_time >= 360 && s_time < 600)
                {
                    return SkillLevel.Level1;
                }
                else
                {
                    return SkillLevel.Failed;
                }

            default:
                return SkillLevel.Failed;
        }
    }
}
