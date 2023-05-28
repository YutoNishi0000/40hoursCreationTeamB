using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : UIController
{
    private const int sceneIndex = 3;
    void Update()
    {
        if (GameManager.Instance.blockSwithScene)
        {
            return;
        }
        MoveScene(sceneIndex);
    }
}
