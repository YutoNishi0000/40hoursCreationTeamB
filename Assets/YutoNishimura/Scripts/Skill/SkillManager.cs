using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//スキルに関する関数

//メモ：スコア加算はきょうやに任せたい

//JudgeScoreコンポーネントを動的に追加したい <- ゲームシーンに移動したとき
public class SkillManager : Actor
{
    private enum SkillType
    {
        AddScore,      //スコア加算
        SpeedUp,       //プレイヤーの移動スピードアップ
        SpeedDown,     //対象の移動スピードダウン
        SeeTargetPos   //対象の位置を示すミニマップ表示
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
    private bool countflag;           //スキル取得カウントフラグ
    private bool targetMinimapFlag;     //ターゲットのミニマップを表示するかどうか
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
    /// スキルカウント開始時この関数を必ず呼び出す
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
    /// スキル発動関数（１フレームだけ呼び出す）
    /// </summary>
    public void SkillImposition()
    {
        switch(GetSkillType())
        {
            case SkillType.AddScore:
                AddScore();
                SkillReset();     //念のためスキルリセットを行っておく
                break;
            case SkillType.SpeedUp:
                PlayerSpeedUp();
                //10秒後にスキルリセット
                Invoke(nameof(SkillReset), 10);
                break;
            case SkillType.SpeedDown:
                TargetSpeedDown();
                //5秒後にスキルリセット
                Invoke(nameof(SkillReset), 5);
                break;
            case SkillType.SeeTargetPos:
                SetTargetMinimapFlag(true);
                //5秒後にスキルリセット
                Invoke(nameof(SkillReset), 5);
                break;
        }

        //タイマーカウントを終了
        EndCount();
    }

    /// <summary>
    /// メモ：Invokeで呼び出す
    /// スキルが重複しないように全てのスキルをリセットする
    /// </summary>
    public void SkillReset()
    {
        playerInstance.SetPlayerSpeed(playerInstance.GetInitialPlayerSpeed());
        targetInstance.SetTargetSpeed(targetInstance.GetInitialTargetSpeed());
        SetTargetMinimapFlag(false);
    }

    /// <summary>
    /// スキルの種類を確率に応じて取得
    /// </summary>
    /// <returns></returns>
    private SkillType GetSkillType()
    {
        //Random.valueは0から1の範囲の値を返す
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
    /// スコア関係をいじりたい
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
    /// スキルレベルを取得
    /// </summary>
    /// <returns>アドレス</returns>
    /// timeはこのクラス内で使いまわしたいー＞メモリ消費削減のため
    private SkillLevel GetSkillLevel(ref float resultTime)
    {
        //参照渡しをして得られた引数のアドレスを動的にコピー
        float s_time = resultTime;

        //アドレスの中の値を初期化
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
