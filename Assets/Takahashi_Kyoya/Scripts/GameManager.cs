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

    /// <summary>
    /// 次の日に行く
    /// </summary>
    /// <param name="sceneName"></param>次のシーンの名前
    /// <param name="canNextDay"></param>次の日に行けるか
    public void NextDay(string name, bool can)
    {
        //次の日に行けるか
        if (can)
        {
            Date++;
            CanNextDay = false;
            FadeManager.Instance.LoadScene(name, 1.0f);
        }
        else
        {
            Date = 0;
            FadeManager.Instance.LoadScene(gameOverScene, 1.0f);
        }
    }
    /// <summary>
    /// ゲームスタート
    /// </summary>
    public void GameStart(string name)
    {
        FadeManager.Instance.LoadScene(name, 1.0f);
    }
    //ゲッター
    public int GetDate()
    {
        return Date;
    }
    public bool GetCanNextDay()
    {
        return CanNextDay;
    }
    //セッター
    public void SetDate(int i)
    {
        Date = i;
    }
    public void SetCanNextDay(bool b)
    {
        CanNextDay = b;
    }
}
