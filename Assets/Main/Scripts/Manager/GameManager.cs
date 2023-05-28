using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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
        Title,
        Home,
        StageSelection,
        MainGame,
        Result
    }

    //�T�u�J�����Ŏʐ^���B������
    public bool IsSubPhoto = false;

    public int numTargetShutter = 0;   //�^�[�Q�b�g���B�e������

    public int numSubShutter = 0;       //�T�u�J�����ŎB�e�����َ��Ȃ��̂̐�

    public int numLowScore = 0;         //�^�[�Q�b�g�B�e��10pt���̕]���̐�
    public int numMiddleScore = 0;      //�^�[�Q�b�g�B�e��30pt���̕]���̐�
    public int numHighScore = 0;        //�^�[�Q�b�g�B�e��50pt���̕]���̐�

    public bool isClear;                //�N���A���Ă��邩�ǂ���

    //�Q�[�����n�܂��Ă��邩
    public bool StartGame = false;

    public List<string> filePathes;

    //�����̓��t
    public int Date = 0;

    public GameMode gameMode;
    public GameState gameState;
    //true���ƃV�[���؂�ւ������Ȃ�
    public bool blockSwithScene = true;

    private const string directoryPath = "Pictures";      //�v���W�F�N�g�t�@�C�������Ƀf�B���N�g�����쐬
    public int sceneIndex;           //�J�ڂ������V�[���̃C���f�b�N�X�ԍ�
    public int numGoToResultScene;  //���U���g�V�[���Ɍ�������

    private void Start()
    {
        numGoToResultScene = 0;
        sceneIndex = 0;
        BGMPlayer();
        isClear = false;
        filePathes = new List<string>();
        gameMode = new GameMode();
        gameState = new GameState();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuit();
        }
    }

    #region BGM�֌W

    public void BGMPlayer()
    {
        switch(sceneIndex)
        {
            case (int)GameState.MainGame:
                BGMManager.Instance.PlayInGameBGM();
                break;
            case (int)GameState.Result:
                BGMManager.Instance.PlayResultBGM();
                break;
            default:
                if (!BGMManager.Instance.GetPlayBGMFLag())
                {
                    BGMManager.Instance.PlayOutGameBGM();
                }
                break;
        }
    }

    #endregion

    #region �t�@�C���֌W

    //�ʐ^��ۑ�����f�B���N�g���p�X���擾����
    public string GetPicturesFilePath(string directryName)
    {
        //�f�B���N�g�������݂��Ă��邩
        if(System.IO.Directory.Exists(directryName))
        {
            Debug.Log("�ʐ^�t�H���_�͑��݂��Ă���");
        }
        else
        {
            Debug.Log("�ʐ^�t�H���_�͑��݂��Ă��Ȃ�");
            //�f�B���N�g���쐬
            System.IO.Directory.CreateDirectory(directryName);
        }

        string picturesFilePath = directryName + "/";

        return picturesFilePath;
    }

    //�ʐ^���i�[���Ă���f�B���N�g�����폜
    public void DestroyPicturesDirectory(string targetDirectoryPath)
    {
        if (!Directory.Exists(targetDirectoryPath))
        {
            return;
        }

        //�f�B���N�g���ȊO�̑S�t�@�C�����폜
        string[] filePaths = Directory.GetFiles(targetDirectoryPath);
        foreach (string filePath in filePaths)
        {
            File.SetAttributes(filePath, FileAttributes.Normal);
            File.Delete(filePath);
        }

        //�Q�[���I�����A�ʐ^���i�[���Ă���f�B���N�g�������݂��Ă���̂Ȃ��
        if (System.IO.Directory.Exists(targetDirectoryPath))
        {
            //�f�B���N�g�����폜
            System.IO.Directory.Delete(targetDirectoryPath);
        }
    }

    //�A�v���P�[�V�����I�����ɌĂяo���֐�
    public void GameQuit()
    {
        DestroyPicturesDirectory(directoryPath);
        Application.Quit();
    }

    #endregion

    public void SetGameState(GameState state) { gameState = state; }

    public GameState GetGameState() { return gameState; }

    public void SetGameMode(GameMode mode) { gameMode = mode; }

    public GameMode GetGameMode() { return gameMode; }

    public string GetDirectryPath() { return directoryPath; }
}