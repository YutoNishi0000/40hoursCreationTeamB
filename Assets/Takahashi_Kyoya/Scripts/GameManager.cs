using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    //�Q�[���I�[�o�[�V�[��
    private string gameOverScene = "GameOver";
    //�����̓��t
    public static int DATE = 0;

    /// <summary>
    /// ���̓��ɍs��
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
