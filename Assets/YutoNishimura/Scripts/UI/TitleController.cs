using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TitleController : UIController
{
    [Header("�z�[����ʂ̃V�[���������Ă�������")]
    [SerializeField] private string HomeSceneName;
    string address = "Assets/Kyoya_Takahashi/Prefabs/OutGame/Animation/SwichAnimationEnd.prefab";
    private GameObject endAnimation = null;
    private void Start()
    {
        endAnimation = AssetDatabase.LoadAssetAtPath<GameObject>(address);
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(endAnimation);
            //MoveScene(HomeSceneName);
            PlaySE();
        }
    }
}
