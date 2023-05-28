using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TitleController : UIController
{
    [Header("ホーム画面のシーン名を入れてください")]
    [SerializeField] private string HomeSceneName;
    //trueだとシーン切り替えをしない
    private bool blockSwithScene = true;
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //MoveScene(HomeSceneName);
            InstantAnimation();
            PlaySE();
        }
        Debug.Log(blockSwithScene);

        if (blockSwithScene)
        {
            return;
        }
        MoveScene(HomeSceneName);
    }
    public void UnBlockSwithScene()
    {
        Debug.Log("呼ばれた");
        blockSwithScene = false;
    }
}
