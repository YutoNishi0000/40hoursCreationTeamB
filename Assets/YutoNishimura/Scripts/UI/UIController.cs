using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;


//UIの基底クラス
//このクラスは特に継承必要がないとき、UI自身にアタッチするものとする
public class UIController : MonoBehaviour
{    
    //シーン切り替え時のアニメーションプレハブのアドレス
    private string address = "Assets/Kyoya_Takahashi/Prefabs/OutGame/Animation/SwichAnimationEnd.prefab";
    //シーン切り替え時のアニメーションプレハブ
    protected GameObject endAnimation = null;

   
    private const int stageSlectIndex = 1;  //ステージ選択シーンのインデックス番号
    private const int homeIndex = 2;        //ホームシーンのインデックス番号
    private const int stageIndex = 3;       //ステージシーンのインデックス番号
    private const int resultIndex = 4;      //リザルトシーンのインデックス番号
    private const int operationIndex = 5;   //オペレーションシーンのインデックス番号

    private void Awake()
    {
#if UNITY_EDITOR
        endAnimation = AssetDatabase.LoadAssetAtPath<GameObject>(address);
#endif
    }
    //シーン移動
    public void MoveScene(int index)
    {
        if (!GameManager.Instance.blockSwithScene)
        {
            GameManager.Instance.GameAdministrator();
            SceneManager.LoadScene(index);
        }
        GameManager.Instance.blockSwithScene = true;         
    }
    public void PlaySelectSE()
    {
        SEManager.Instance.PlaySelect();
    }
    public void PlayDecisionSE()
    {
        SEManager.Instance.PlayDecision();
    }
    public void PlayBackSE()
    {
        SEManager.Instance.PlayBack();
    }
    public void UnBlockSwithScene()
    {
        GameManager.Instance.blockSwithScene = false;
    }
    public void InstantAnimation()
    {
        Instantiate(GameManager.Instance.animations[(int)GameManager.ShutterAnimationState.End]);
    }
    
    public void StageSlectScene()
    {
        GameManager.Instance.sceneIndex = homeIndex;
    }
    public void HomeScene()
    {
        GameManager.Instance.sceneIndex = stageSlectIndex;
    }
    public void StageScene()
    {
        GameManager.Instance.sceneIndex = stageIndex;
    }
    public void ResultScene()
    {
        Debug.Log("リザルト");
        GameManager.Instance.sceneIndex = resultIndex;
    }
    public void OperationScene()
    {
        GameManager.Instance.sceneIndex = operationIndex;
    }
}
