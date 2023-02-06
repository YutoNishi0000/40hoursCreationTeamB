using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    //ゲームオーバーシーン
    private string gameOverScene = "GameOver";
    //今日の日付
    public int Date = 0;
    //次の日に行けるか
    public bool CanNextDay = false;
    //接触範囲に入っているか
    public bool inContactArea = false;

    /// <summary>
    /// 次の日に行く
    /// </summary>
    /// <param name="sceneName"></param>次のシーンの名前
    public void NextDay(string name)
    {
        Date++;
        FadeManager.Instance.LoadScene(name, 1.0f);
    }
    public void GameOver()
    {
        Date = 0;
        FadeManager.Instance.LoadScene(name, 1.0f);
    }

    /// <summary>
    /// アウトゲームのシーン切り替え
    /// </summary>
    public void OutGameNextScene(string name)
    {
        FadeManager.Instance.LoadScene(name, 1.0f);
    }
    //===== ゲッター =====
    public int GetDate()
    {
        return Date;
    }
    public bool GetCanNextDay()
    {
        return CanNextDay;
    }
    public bool GetInContactArea()
    {
        return inContactArea;
    }
    //===== セッター =====
    public void SetDate(int i)
    {
        Date = i;
    }
    public void SetCanNextDay(bool b)
    {
        CanNextDay = b;
    }
    public void SetInContactArea(bool b)
    {
        inContactArea = b;
    }
}
