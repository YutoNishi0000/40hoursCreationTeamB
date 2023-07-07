using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : UIController
{
    void Update()
    {
        if (GameManager.Instance.blockSwithScene)
        {
            return;
        }
        MoveScene(GameManager.Instance.sceneIndex);
    }

    public void SetGameMode_Easy()
    {
        GameManager.Instance.SetGameMode(GameManager.GameMode.Easy);
    }

    public void SetGameMode_Nomal()
    {
        GameManager.Instance.SetGameMode(GameManager.GameMode.Nomal);
    }

    public void SetGameMode_Hard()
    {
        GameManager.Instance.SetGameMode(GameManager.GameMode.Hard);
    }
}
