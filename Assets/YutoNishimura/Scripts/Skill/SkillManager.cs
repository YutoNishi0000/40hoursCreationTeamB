using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//スキルに関する関数

//JudgeScoreコンポーネントを動的に追加したい <- ゲームシーンに移動したとき
public class SkillManager : Actor
{
    private static bool addScoreFlag;         //スコア加算フラグ
    private bool targetMinimapFlag;     //ターゲットのミニマップを表示するかどうか
    private bool skillBlock_player;
    private bool skillBlock_addScore;
    private bool skillBlock_seeTarget;
    private float time;
    [SerializeField] private readonly float interval = Config.intervalActiveTargetMInimap;
    private int shutterTimeStamp;
    private bool minimapSkillFlag;         //ターゲットのミニマップの表示フラグのために使う
    private float playerAccelSpeed;
    private readonly float accelerationSpeed = Config.raisePlayerSpeed;   //プレイヤーのスキル獲得時の速度倍率
    private readonly int minimapTargetShutterNum = 5;   //何枚おきにスキルが発動するか
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
    /// スキルロック解除
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
    /// スキル発動関数
    /// </summary>
    public void SkillImposition()
    {
        if(skillBlock_player)
        {
            return;
        }

        //プレイヤー速度アップ開放
        PlayerSpeedUp();

        if (skillBlock_addScore)
        {
            return;
        }

        //スコアアップ開放
        SetSpiritSenceFlag(true);

        if (skillBlock_seeTarget)
        {
            return;
        }

        //ターゲット可視化解放
        TargetMinimapActivation();
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    private void AddScore()
    {
        SetSpiritSenceFlag(true);
    }

    /// <summary>
    /// ターゲットのミニマップを表示する
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
    /// プレイヤーの移動速度アップ
    /// </summary>
    private void PlayerSpeedUp()
    {
        playerAccelSpeed = player.GetInitialPlayerSpeed() * accelerationSpeed;
        playerInstance.SetPlayerSpeed(playerAccelSpeed);
    }

    //ターゲットのミニマップを表示するかどうか
    public void SetTargetMinimapFlag(bool flag) { targetMinimapFlag = flag; }

    public bool GetTargetMinimapFlag() { return targetMinimapFlag; }

    //スコア加算フラグ
    public static void SetSpiritSenceFlag(bool flag) { addScoreFlag = flag; }

    public static bool GetSpiritSenceFlag() { return addScoreFlag; }

    // スピード
    public bool GetPlayerSpeedFlag() { return skillBlock_player; }

}
