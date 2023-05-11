using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SkillManager))]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public enum GameMode
    {
        Easy,
        Nomal,
        Hard
    }

    public enum GameState
    {

    }

    //�ʐ^���B������
    public bool IsPhoto = false;

    //�T�u�J�����Ŏʐ^���B������
    public bool IsSubPhoto = false;

    public int numSubShutter = 0;       //�T�u�J�����ŎB�e�����َ��Ȃ��̂̐�

    //�^�C�g���̊����摜
    public GameObject TitleUI;
    //�Q�[���I�[�o�[���ǂ���
    public bool gameOver = false;

    //�Q�[���I�[�o�[�V�[��
    private string gameOverScene = "GameOver";
    //�Q�[���N���A�V�[��
    private string gameClearScene = "GameClear";
    //�����̓��t
    public int Date = 0;

    public GameMode gameMode;
    public SkillManager skillManager;

    private void Start()
    {
        gameMode = new GameMode();
        skillManager = GetComponent<SkillManager>();
    }



    //�Q�b�^�[
    public bool GetGameOver()
    {
        return gameOver;
    }
    //�Z�b�^�[
    public void SetGameOver(bool b)
    {
        gameOver = b;
    }

    public GameMode GetGameMode() { return gameMode; }

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