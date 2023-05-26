using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[RequireComponent(typeof(SkillManager))]
[RequireComponent(typeof(HeterogeneousSetter))]
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
    //�T�u�J�����Ŏʐ^���B������
    public bool IsSubPhoto = false;

    public int numTargetShutter = 0;   //�^�[�Q�b�g���B�e������

    public int numSubShutter = 0;       //�T�u�J�����ŎB�e�����َ��Ȃ��̂̐�

    //�Q�[�����n�܂��Ă��邩
    public bool StartGame = false;

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
    public HeterogeneousSetter strangeSetter;

    private const string directoryPath = "Pictures";      //�v���W�F�N�g�t�@�C�������Ƀf�B���N�g�����쐬
    private string picturesFilePath;                       //�t�@�C���p�X���w�肷�邽�߂̂���

    private void Start()
    {
        gameMode = new GameMode();
        skillManager = GetComponent<SkillManager>();
        strangeSetter = GetComponent<HeterogeneousSetter>();
    }

    private void Update()
    {
        
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

    #region �t�@�C���֌W

    //�ʐ^��ۑ�����f�B���N�g���p�X���擾����
    public string GetPicturesFilePath()
    {
        //�f�B���N�g�������݂��Ă��邩
        if(System.IO.Directory.Exists(directoryPath))
        {
            Debug.Log("�ʐ^�t�H���_�͑��݂��Ă���");
        }
        else
        {
            Debug.Log("�ʐ^�t�H���_�͑��݂��Ă��Ȃ�");
            //�f�B���N�g���쐬
            System.IO.Directory.CreateDirectory(directoryPath);
        }

        picturesFilePath = directoryPath + "/";

        return picturesFilePath;
    }

    //�ʐ^���i�[���Ă���f�B���N�g�����폜
    public void DestroyPicturesDirectory()
    {
        //�Q�[���I�����A�ʐ^���i�[���Ă���f�B���N�g�������݂��Ă���̂Ȃ��
        if(System.IO.Directory.Exists(directoryPath))
        {
            //�f�B���N�g�����폜
            System.IO.Directory.Delete(directoryPath);
        }
    }

    #endregion

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