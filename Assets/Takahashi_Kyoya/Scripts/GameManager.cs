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
    //���̓��ɍs���邩
    public bool CanNextDay = false;
    //�ڐG�͈͂ɓ����Ă��邩
    public bool inContactArea = false;

    /// <summary>
    /// ���̓��ɍs��
    /// </summary>
    /// <param name="sceneName"></param>���̃V�[���̖��O
    /// <param name="canNextDay"></param>���̓��ɍs���邩
    public void NextDay(string name, bool can)
    {
        //���̓��ɍs���邩
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
    /// �A�E�g�Q�[���̃V�[���؂�ւ�
    /// </summary>
    public void OutGameNextScene(string name)
    {
        FadeManager.Instance.LoadScene(name, 1.0f);
    }
    //===== �Q�b�^�[ =====
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
    //===== �Z�b�^�[ =====
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
