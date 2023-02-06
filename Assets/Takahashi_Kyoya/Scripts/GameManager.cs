using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    //ゲームオーバーシーン
    private string gameOverScene = "GameOver";
    //今日の日付
    public static int DATE = 0;

    /// <summary>
    /// 次の日に行く
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="feeling"></param>
    public void NextDay(string sceneName, bool canNextDay)
    {
        if (canNextDay)
        {
            DATE++;
            FadeManager.Instance.LoadScene(sceneName, 1.0f);
        }
        else
        {
            FadeManager.Instance.LoadScene(gameOverScene, 1.0f);
        }
    }
}
