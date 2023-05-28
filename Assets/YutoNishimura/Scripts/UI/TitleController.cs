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
            InstantAnimation();
            PlaySE();
        }
        Debug.Log(GameManager.Instance.blockSwithScene);

        if (GameManager.Instance.blockSwithScene)
        {
            return;
        }
        MoveScene(HomeSceneName);
    }
}
