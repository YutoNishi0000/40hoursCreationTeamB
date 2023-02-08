using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TASK
{
    //�\������^�X�N�e�L�X�g
    public string taskName;
    //�^�X�N���������Ă��邩
    public bool isCompletion;
    //���̓��Ɏ����z���邩
    public bool takeOver;
    //�����ڂ̃^�X�N��
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
    //�^�X�N�N���X
    public List<TASK> tasks = new List<TASK>()
    {
        //day1
        new TASK { taskName = "�h���̐l�h�������悤\n(�ł�������ȁI)",
            isCompletion = false, takeOver = true , date = 0 },
        //day2
        new TASK { taskName = "�΂�Ȃ悤�Ɏʐ^��\n�B�낤",
            isCompletion = false, takeOver = true , date = 1 },
        //day3
        new TASK { taskName = "�H�H�H\n(�Ƃ肠�����h���̐l�h��T����)",
            isCompletion = false, takeOver = false, date = 2 },
        //day4
        new TASK { taskName = "�n���J�`��Ԃ���",
            isCompletion = false, takeOver = false, date = 3 },
        //day5
        new TASK { taskName = "�h���̐l�h�ɘb�������悤",
            isCompletion = false, takeOver = false, date = 4 },
    };
    //�Q�b�^�[
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

    //�Z�b�^�[

    //�Q�[���I�[�o�[�V�[��
    private string gameOverScene = "GameOver";
    //�Q�[���N���A�V�[��
    private string gameClearScene = "GameClear";
    //�����̓��t
    public int Date = 0;
    //���̓��ɍs���邩
    public bool CanNextDay = false;
    //�ڐG�͈͂ɓ����Ă��邩
    public bool inContactArea = false;

    private void Start()
    {
        //Date = 0;
    }

    /// <summary>
    /// ���̓��ɍs��
    /// </summary>
    /// <param name="sceneName"></param>���̃V�[���̖��O
    public void NextDay(string name)
    {
        Date++;
        FadeManager.Instance.LoadScene(name, 1.0f);
    }
    /// <summary>
    /// �Q�[���I�[�o�[�V�[���ɐ؂�ւ�
    /// </summary>
    public void GameOver()
    {
        Date = 0;
        FadeManager.Instance.LoadScene(gameOverScene, 1.0f);
    }
    /// <summary>
    /// �Q�[���N���A�V�[���ɐ؂�ւ�
    /// </summary>
    public void GameClear()
    {
        Date = 0;
        FadeManager.Instance.LoadScene(gameClearScene, 1.0f);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    // �A�v���P�[�V�������I��������(�A�v���P�[�V�����I���̃R�[�h�����U����̖h�����߂�public�֐�)
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif        
    }
}
