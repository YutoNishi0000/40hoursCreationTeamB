using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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

    private const string directoryPath = "Pictures";      //プロジェクトファイル直下にディレクトリを作成
    private string picturesFilePath;                       //ファイルパスを指定するためのもの

    private void Start()
    {
        gameMode = new GameMode();
        skillManager = GetComponent<SkillManager>();
        strangeSetter = GetComponent<HeterogeneousSetter>();
    }

    private void Update()
    {
        
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
    public string GetPicturesFilePath()
    {
        //ディレクトリが存在しているか
        if(System.IO.Directory.Exists(directoryPath))
        {
            Debug.Log("写真フォルダは存在している");
        }
        else
        {
            Debug.Log("写真フォルダは存在していない");
            //ディレクトリ作成
            System.IO.Directory.CreateDirectory(directoryPath);
        }

        picturesFilePath = directoryPath + "/";

        return picturesFilePath;
    }

    //写真を格納しているディレクトリを削除
    public void DestroyPicturesDirectory()
    {
        //ゲーム終了時、写真を格納しているディレクトリが存在しているのならば
        if(System.IO.Directory.Exists(directoryPath))
        {
            //ディレクトリを削除
            System.IO.Directory.Delete(directoryPath);
        }
    }

    #endregion

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