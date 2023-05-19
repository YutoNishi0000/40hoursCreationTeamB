using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SkillManager))]
[RequireComponent(typeof(HeterogeneousSetter))]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public enum GameMode
    {
        Easy,
        Nomal,
        Hard
    }

    public enum GameState
    {

    }
    //サブカメラで写真を撮ったか
    public bool IsSubPhoto = false;

    public int numTargetShutter = 0;   //ターゲットを撮影した回数

    public int numSubShutter = 0;       //サブカメラで撮影した異質なものの数

    //ゲームが始まっているか
    public bool StartGame = false;

    //タイトルの割れる画像
    public GameObject TitleUI;
    //ゲームオーバーかどうか
    public bool gameOver = false;

    //ゲームオーバーシーン
    private string gameOverScene = "GameOver";
    //ゲームクリアシーン
    private string gameClearScene = "GameClear";
    //今日の日付
    public int Date = 0;

    public GameMode gameMode;
    public SkillManager skillManager;
    public HeterogeneousSetter strangeSetter;

    private void Start()
    {
        gameMode = new GameMode();
        skillManager = GetComponent<SkillManager>();
        strangeSetter = GetComponent<HeterogeneousSetter>();
    }



    //ゲッター
    public bool GetGameOver()
    {
        return gameOver;
    }
    //セッター
    public void SetGameOver(bool b)
    {
        gameOver = b;
    }

    public GameMode GetGameMode() { return gameMode; }

    // アプリケーションを終了させる(アプリケーション終了のコードが分散するの防ぐためにpublic関数)
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif        
    }
}