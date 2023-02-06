using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    //�Q�[���I�[�o�[�V�[��
    private string gameOverScene = "GameOver";
    //�����̓��t
    public int Date = 0;

    /// <summary>
    /// ���̓��ɍs��
    /// </summary>
    /// <param name="sceneName"></param>���̃V�[���̖��O
    /// <param name="canNextDay"></param>���̓��ɍs���邩(�^�X�N���I����Ă��邩)
    public void NextDay(string sceneName, bool canNextDay)
    {
        //���̓��ɍs���邩
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
    //�Q�b�^�[
    public int GetDate()
    {
        return Date;
    }
}
