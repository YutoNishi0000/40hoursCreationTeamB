using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : UIController
{
    private int sceneIndex = 0;         //�J�ڂ������V�[���̃C���f�b�N�X�ԍ�
    private const int stageIndex = 3;   //�X�e�[�W�V�[���̃C���f�b�N�X�ԍ�
    private const int homeIndex = 1;    //�z�[���V�[���̃C���f�b�N�X�ԍ�
    private void Start()
    {
        sceneIndex = stageIndex;
    }
    void Update()
    {
        if (GameManager.Instance.blockSwithScene)
        {
            return;
        }
        MoveScene(sceneIndex);
    }
    public void backHome()
    {
        sceneIndex = homeIndex;
    }
}
