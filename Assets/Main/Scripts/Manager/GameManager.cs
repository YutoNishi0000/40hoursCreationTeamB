using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    //ゲームの難易度
    public enum GameMode
    {
        Easy,
        Nomal,
        Hard
    }

    //ゲームが今どのような状態なのか
    public enum GameState
    {
        Title,
        Home,
        StageSelection,
        MainGame,
        Result,
        Operator
    }

    public enum ShutterAnimationState
    {
        None,
        Other,
        Target,
        End
    }

    //サブカメラで写真を撮ったか
    public bool IsSubPhoto = false;

    public int numTargetShutter = 0;   //ターゲットを撮影した回数

    public int numSubShutter = 0;       //サブカメラで撮影した異質なものの数

    public int numLowScore = 0;         //ターゲット撮影時10pt文の評価の数
    public int numMiddleScore = 0;      //ターゲット撮影時30pt文の評価の数
    public int numHighScore = 0;        //ターゲット撮影時50pt文の評価の数

    
    public bool IsPlayGame = false;     //プレイヤーがゲームプレイ可能か

    public List<string> filePathes;

    public GameMode gameMode;
    public GameState gameState;
    //trueだとシーン切り替えをしない
    public bool blockSwithScene = true;

    private const string directoryPath = "Pictures";      //プロジェクトファイル直下にディレクトリを作成
    public int sceneIndex;           //遷移したいシーンのインデックス番号

    public GameObject[] animations;

    private void Start()
    {
        sceneIndex = 0;
        GameAdministrator();
        filePathes = new List<string>();
        gameMode = new GameMode();
        gameState = new GameState();

        BGMManager.Instance.audioSource.Stop();
        BGMManager.Instance.PlayOutGameBGM();
    }

    private void Update()
    {

    }

    #region BGM関係

    public void GameAdministrator()
    {
        switch (sceneIndex)
        {
            case (int)GameState.MainGame:
                InitializeGame();
                BGMManager.Instance.PlayInGameBGM();
                break;
            case (int)GameState.Result:
                BGMManager.Instance.PlayResultBGM();
                break;
            case (int)GameState.Operator:
                BGMManager.Instance.StopBGM();
                break;
            default:
                if (!BGMManager.Instance.audioSource.isPlaying || BGMManager.Instance.audioSource.clip != BGMManager.Instance.BGM[(int)BGMManager.BGMTyoe.OutGame])
                {
                    BGMManager.Instance.PlayOutGameBGM();
                }
                break;
        }
    }

    public void InitializeGame()
    {
        numTargetShutter = 0;
        numSubShutter = 0;
        numLowScore = 0;
        numLowScore = 0;
        numMiddleScore = 0;
        numHighScore = 0;
        IsPlayGame = false;
        ScoreManger.Score = 0;
    }

    #endregion

    #region ファイル関係

    //写真を保存するディレクトリパスを取得する
    public string GetPicturesFilePath(string directryName)
    {
        //ディレクトリが存在しているか
        if(System.IO.Directory.Exists(directryName))
        {
            Debug.Log("写真フォルダは存在している");
        }
        else
        {
            Debug.Log("写真フォルダは存在していない");
            //ディレクトリ作成
            System.IO.Directory.CreateDirectory(directryName);
        }

        string picturesFilePath = directryName + "/";

        return picturesFilePath;
    }

    //写真を格納しているディレクトリを削除
    public void DestroyPicturesDirectory(string targetDirectoryPath)
    {
        if (!Directory.Exists(targetDirectoryPath))
        {
            return;
        }

        //ディレクトリ以外の全ファイルを削除
        string[] filePaths = Directory.GetFiles(targetDirectoryPath);
        foreach (string filePath in filePaths)
        {
            File.SetAttributes(filePath, FileAttributes.Normal);
            File.Delete(filePath);
        }

        //ゲーム終了時、写真を格納しているディレクトリが存在しているのならば
        if (System.IO.Directory.Exists(targetDirectoryPath))
        {
            //ディレクトリを削除
            System.IO.Directory.Delete(targetDirectoryPath);
        }
    }

    //アプリケーション終了時に呼び出す関数
    public void GameQuit()
    {
        DestroyPicturesDirectory(directoryPath);
    }

    #endregion

    public void SetGameState(GameState state) { gameState = state; }

    public GameState GetGameState() { return gameState; }

    public void SetGameMode(GameMode mode) { gameMode = mode; }

    public GameMode GetGameMode() { return gameMode; }

    public string GetDirectryPath() { return directoryPath; }
}