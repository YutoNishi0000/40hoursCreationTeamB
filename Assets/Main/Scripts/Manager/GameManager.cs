using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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
        Title,
        Home,
        StageSelection,
        MainGame,
        Result
    }

    //サブカメラで写真を撮ったか
    public bool IsSubPhoto = false;

    public int numTargetShutter = 0;   //ターゲットを撮影した回数

    public int numSubShutter = 0;       //サブカメラで撮影した異質なものの数

    public int numLowScore = 0;         //ターゲット撮影時10pt文の評価の数
    public int numMiddleScore = 0;      //ターゲット撮影時30pt文の評価の数
    public int numHighScore = 0;        //ターゲット撮影時50pt文の評価の数

    public bool isClear;                //クリアしているかどうか

    //ゲームが始まっているか
    public bool StartGame = false;

    //ゲームオーバーかどうか
    public bool gameOver = false;

    public List<string> filePathes;

    //ゲームオーバーシーン
    private string gameOverScene = "GameOver";
    //ゲームクリアシーン
    private string gameClearScene = "GameClear";
    //今日の日付
    public int Date = 0;

    public GameMode gameMode;
    public GameState gameState;

    private const string directoryPath = "Pictures";      //プロジェクトファイル直下にディレクトリを作成

    private void Start()
    {
        isClear = false;
        filePathes = new List<string>();
        gameMode = new GameMode();
        gameState = new GameState();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuit();
        }
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
        Application.Quit();
    }

    #endregion

    public void SetGameState(GameState state) { gameState = state; }

    public GameState GetGameState() { return gameState; }

    public void SetGameMode(GameMode mode) { gameMode = mode; }

    public GameMode GetGameMode() { return gameMode; }

    public string GetDirectryPath() { return directoryPath; }

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