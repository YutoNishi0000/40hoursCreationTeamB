using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : UIController
{
    [Header("�z�[����ʂ̃V�[���������Ă�������")]
    [SerializeField] private string HomeSceneName;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveScene(HomeSceneName);
            PlaySE();
        }
    }
}
