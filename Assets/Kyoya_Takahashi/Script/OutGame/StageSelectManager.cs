using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : UIController
{
    private int sceneIndex = 0;         //遷移したいシーンのインデックス番号
    private const int stageIndex = 3;   //ステージシーンのインデックス番号
    private const int homeIndex = 1;    //ホームシーンのインデックス番号
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
