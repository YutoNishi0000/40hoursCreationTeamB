using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string gameOverScene = "GameOver";

    /// <summary>
    /// Ÿ‚Ì“ú‚És‚­
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="feeling"></param>
    public void NextDay(string sceneName, bool canNextDay)
    {
        if (canNextDay)
        {
            FadeManager.Instance.LoadScene(sceneName, 1.0f);
        }
        else
        {
            FadeManager.Instance.LoadScene(gameOverScene, 1.0f);
        }
    }


}
