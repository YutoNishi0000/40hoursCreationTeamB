using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ShutterAnimation : MonoBehaviour
{
    string address0 = "Assets/Kyoya_Takahashi/Prefabs/Animation/OtherAnimation.prefab";
    string address1 = "Assets/Kyoya_Takahashi/Prefabs/Animation/TargetAnimation.prefab";
    //0:other, 1:target
    static GameObject[] Animation = new GameObject[2];
    private void Start()
    {
        //Animation[0] = AssetDatabase.LoadAssetAtPath<GameObject>(address0);
        //Animation[1] = AssetDatabase.LoadAssetAtPath<GameObject>(address1);
    }


    /// <summary>
    /// ターゲットアニメーション開始
    /// </summary>
    public static void TargetAnimationStart()
    {

        //Instantiate(Animation[1]);
    }
    /// <summary>
    /// その他アニメーション開始
    /// </summary>
    public static void OtherAnimationStart()
    {
        //Instantiate(Animation[0]);
    }
}
