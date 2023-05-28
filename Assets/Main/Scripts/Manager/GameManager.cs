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

    public List<string> filePathes;

    //今日の日付
    public int Date = 0;

    public GameMode gameMode;
    public GameState gameState;
    //trueだとシーン切り替えをしない
    public bool blockSwithScene = true;

    private const string directoryPath = "Pictures";      //プロジェクトファイル直下にディレクトリを作成
    public int sceneIndex;           //遷移したいシーンのインデックス番号
    public int numGoToResultScene;  //リザルトシーンに言った回数

    private void Start()
    {
        numGoToResultScene = 0;
        sceneIndex = 0;
        BGMPlayer();
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

    #region BGM関係

    public void BGMPlayer()
    {
        switch(sceneIndex)
        {
            case (int)GameState.MainGame:
                BGMManager.Instance.PlayInGameBGM();
                break;
            case (int)GameState.Result:
                BGMManager.Instance.PlayResultBGM();
                break;
            default:
                if (!BGMManager.Instance.GetPlayBGMFLag())
                {
                    BGMManager.Instance.PlayOutGameBGM();
                }
                break;
        }
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
        Application.Quit();
    }

    #endregion

    public void SetGameState(GameState state) { gameState = state; }

    public GameState GetGameState() { return gameState; }

    public void SetGameMode(GameMode mode) { gameMode = mode; }

    public GameMode GetGameMode() { return gameMode; }

    public string GetDirectryPath() { return directoryPath; }
}