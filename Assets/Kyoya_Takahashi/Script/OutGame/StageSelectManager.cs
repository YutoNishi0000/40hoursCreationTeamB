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
}
