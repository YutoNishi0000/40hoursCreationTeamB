using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ShutterAnimation : MonoBehaviour
{
    private static GameManager.ShutterAnimationState animationState;
    [SerializeField] private GameObject parentAnimationObject;

    private void Start()
    {
        animationState = new GameManager.ShutterAnimationState();
    }

    public void StartAnimation(GameManager.ShutterAnimationState state)
    {
        GameObject animation = new GameObject();

        switch (state)
        {
            case GameManager.ShutterAnimationState.None:
                animation = Instantiate(GameManager.Instance.animations[(int)GameManager.ShutterAnimationState.None], parentAnimationObject.transform);
                animation.GetComponent<Image>().rectTransform.localPosition = Vector3.zero;
                break;
            case GameManager.ShutterAnimationState.Target:
                animation = Instantiate(GameManager.Instance.animations[(int)GameManager.ShutterAnimationState.Target], parentAnimationObject.transform);
                animation.GetComponent<Image>().rectTransform.localPosition = Vector3.zero;
                break;
            case GameManager.ShutterAnimationState.Other:
                animation = Instantiate(GameManager.Instance.animations[(int)GameManager.ShutterAnimationState.Other], parentAnimationObject.transform);
                animation.GetComponent<Image>().rectTransform.localPosition = Vector3.zero;
                break;
        }
    }

    ///// <summary>
    ///// 異質なもの撮影アニメーション開始
    ///// </summary>
    //public static void OtherAnimationStart() 
    //{
    //    Debug.Log("異質");
    //    Instantiate(GameManager.Instance.animations[(int)GameManager.ShutterAnimationState.Other]);
    //}

    ///// <summary>
    ///// ターゲット撮影アニメーション開始
    ///// </summary>
    //public static void TargetAnimationStart()
    //{
    //    Debug.Log("ターゲット");
    //    Instantiate(GameManager.Instance.animations[(int)GameManager.ShutterAnimationState.Target]);
    //}

    ///// <summary>
    ///// 何も撮影できなかったアニメーション開始
    ///// </summary>
    //public static void NoneAnimationStart()
    //{
    //    Debug.Log("何も");
    //    Instantiate(GameManager.Instance.animations[(int)GameManager.ShutterAnimationState.None]);
    //}
}
