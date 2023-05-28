using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TitleController : UIController
{
    [Header("�z�[����ʂ̃V�[���������Ă�������")]
    [SerializeField] private string HomeSceneName;
    //true���ƃV�[���؂�ւ������Ȃ�
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
        Debug.Log("�Ă΂ꂽ");
        blockSwithScene = false;
    }
}
