using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OperationController : UIController
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            HomeScene();
            InstantAnimation();
        }
        MoveScene(GameManager.Instance.sceneIndex);
    }
}
