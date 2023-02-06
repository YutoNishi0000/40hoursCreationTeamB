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

    /// <summary>
    /// 次の日に行く
    /// </summary>
    /// <param name="sceneName"></param>次のシーンの名前
    /// <param name="canNextDay"></param>次の日に行けるか(タスクが終わっているか)
    public void NextDay(string sceneName, bool canNextDay)
    {
        //次の日に行けるか
        if (canNextDay)
        {
            Date++;
            FadeManager.Instance.LoadScene(sceneName, 1.0f);
        }
        else
        {
            Date = 0;
            FadeManager.Instance.LoadScene(gameOverScene, 1.0f);
        }
    }
    //ゲッター
    public int GetDate()
    {
        return Date;
    }
}
