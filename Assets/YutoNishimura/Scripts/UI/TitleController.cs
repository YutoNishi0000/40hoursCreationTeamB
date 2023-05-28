using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


public class TitleController : UIController
{

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //MoveScene(HomeSceneName);
            InstantAnimation();
            PlayDecisionSE();
            HomeScene();
        }

        MoveScene(GameManager.Instance.sceneIndex);
    }
}
