using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    #region シングルトン化

    public static GameSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    //ゲームの進行状況をステートで管理
    public enum GameState
    {
        INITIAL,               //ゲーム開始時
        COUNTDOWN,             //カウントダウン
        GAMESTART,             //ゲームスタート
        NONE_TARGET,           //カメラに何も移っていない状態
        GET_HETEROGENEOUS,     //異質なものがカメラに映った
        GET_TARGET,            //ターゲットがカメラに映った
        CALC_POINT,            //ポイント計算
        RESULT                 //リザルト画面
    }

    private GameState gameState = new GameState();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NextPhase()
    {
        switch(gameState)
        {
            case GameState.INITIAL:
                SetGameState(GameState.COUNTDOWN);
                break;
            case GameState.COUNTDOWN:
                SetGameState(GameState.GAMESTART);
                break;
            case GameState.GAMESTART:
                SetGameState(GameState.NONE_TARGET);
                break;
            //case GameState.NONE_TARGET:
            //    break;
            //case GameState.GET_HETEROGENEOUS:
            //    break;
            //case GameState.CALC_POINT:
            //    SetGameState(GameState.RESULT);
            //    break;
            case GameState.RESULT:
                break;
        }
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
    }

    public GameState GetGameState()
    {
        return gameState;
    }
}
