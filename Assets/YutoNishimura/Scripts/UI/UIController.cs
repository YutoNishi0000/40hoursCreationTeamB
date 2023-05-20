using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

//UIの基底クラス
//このクラスは特に継承必要がないとき、UI自身にアタッチするものとする
public class UIController : MonoBehaviour
{
    //シーン移動
    public void MoveScene(string scene) { SceneManager.LoadScene(scene); }

    public void PlaySE()
    {
        //サウンドを鳴らす
    }
}
