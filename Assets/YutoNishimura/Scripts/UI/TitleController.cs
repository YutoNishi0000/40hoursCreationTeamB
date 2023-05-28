using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


public class TitleController : UIController
{
    private const int sceneIndex = 1;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //MoveScene(HomeSceneName);
            InstantAnimation();
            SEManager.Instance.PlayDecision();
        }

        if (GameManager.Instance.blockSwithScene)
        {
            return;
        }
        MoveScene(sceneIndex);
    }
}
