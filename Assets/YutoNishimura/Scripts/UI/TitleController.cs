using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TitleController : UIController
{
    [Header("�z�[����ʂ̃V�[���������Ă�������")]
    [SerializeField] private string HomeSceneName;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //MoveScene(HomeSceneName);
            UnBlockSwithScene();
            PlaySE();
        }
        if (blockSwithScene)
        {
            return;
        }
        MoveScene(HomeSceneName);

    }
}
