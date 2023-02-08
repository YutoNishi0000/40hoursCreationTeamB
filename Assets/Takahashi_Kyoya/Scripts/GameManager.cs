using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TASK
{
    //表示するタスクテキスト
    public string taskName;
    //タスクが完了しているか
    public bool isCompletion;
    //次の日に持ち越せるか
    public bool takeOver;
    //何日目のタスクか
    public int date;

    public string GetTaskName()
    {
        return taskName;
    }
    public bool GetIsCompletion()
    {
        return isCompletion;
    }
    public void CompletionTask()
    {
        isCompletion = true;
    }
}
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    //タスククラス
    public List<TASK> tasks = new List<TASK>()
    {
        //day1
        new TASK { taskName = "”あの人”を見つけよう\n(でも見つかるな！)",
            isCompletion = false, takeOver = true , date = 0 },
        //day2
        new TASK { taskName = "ばれなように写真を\n撮ろう",
            isCompletion = false, takeOver = true , date = 1 },
        //day3
        new TASK { taskName = "？？？\n(とりあえず”あの人”を探そう)",
            isCompletion = false, takeOver = false, date = 2 },
        //day4
        new TASK { taskName = "ハンカチを返そう",
            isCompletion = false, takeOver = false, date = 3 },
        //day5
        new TASK { taskName = "”あの人”に話しかけよう",
            isCompletion = false, takeOver = false, date = 4 },
    };
    //ゲッター
    public bool GetTakeOver(int idx)
    {
        return tasks[idx].takeOver;
    }
    public bool GetIsCompletion(int idx)
    {
        return tasks[idx].isCompletion;
    }
    public int GetCount()
    {
        return tasks.Count;
    }
    public int GetTaskDate(int idx)
    {
        return tasks[idx].date;
    }
    public string GetTaskName(int idx)
    {
        return tasks[idx].taskName;
    }
    public void SetCompletionTask(int idx)
    {
        tasks[idx].CompletionTask();
    }

    //セッター

    //ゲームオーバーシーン
    private string gameOverScene = "GameOver";
    //ゲームクリアシーン
    private string gameClearScene = "GameClear";
    //今日の日付
    public int Date = 0;
    //次の日に行けるか
    public bool CanNextDay = false;
    //接触範囲に入っているか
    public bool inContactArea = false;

    private void Start()
    {
        //Date = 0;
    }

    /// <summary>
    /// 次の日に行く
    /// </summary>
    /// <param name="sceneName"></param>次のシーンの名前
    public void NextDay(string name)
    {
        Date++;
        FadeManager.Instance.LoadScene(name, 1.0f);
    }
    /// <summary>
    /// ゲームオーバーシーンに切り替え
    /// </summary>
    public void GameOver()
    {
        Date = 0;
        FadeManager.Instance.LoadScene(gameOverScene, 1.0f);
    }
    /// <summary>
    /// ゲームクリアシーンに切り替え
    /// </summary>
    public void GameClear()
    {
        Date = 0;
        FadeManager.Instance.LoadScene(gameClearScene, 1.0f);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

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
