using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    #region �V���O���g����

    public static GameSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    //�Q�[���̐i�s�󋵂��X�e�[�g�ŊǗ�
    public enum GameState
    {
        INITIAL,               //�Q�[���J�n��
        COUNTDOWN,             //�J�E���g�_�E��
        GAMESTART,             //�Q�[���X�^�[�g
        NONE_TARGET,           //�J�����ɉ����ڂ��Ă��Ȃ����
        GET_HETEROGENEOUS,     //�َ��Ȃ��̂��J�����ɉf����
        GET_TARGET,            //�^�[�Q�b�g���J�����ɉf����
        CALC_POINT,            //�|�C���g�v�Z
        RESULT                 //���U���g���
    }

    private GameState gameState = new GameState();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NextPhase()
    {
        switch(gameState)
        {
            case GameState.INITIAL:
                SetGameState(GameState.COUNTDOWN);
                break;
            case GameState.COUNTDOWN:
                SetGameState(GameState.GAMESTART);
                break;
            case GameState.GAMESTART:
                SetGameState(GameState.NONE_TARGET);
                break;
            //case GameState.NONE_TARGET:
            //    break;
            //case GameState.GET_HETEROGENEOUS:
            //    break;
            //case GameState.CALC_POINT:
            //    SetGameState(GameState.RESULT);
            //    break;
            case GameState.RESULT:
                break;
        }
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
    }

    public GameState GetGameState()
    {
        return gameState;
    }
}
