using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistController : MonoBehaviour
{
    //ゲーム終了時に呼び出す
    public void EndGame()
    {
        GameManager.Instance.GameQuit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
