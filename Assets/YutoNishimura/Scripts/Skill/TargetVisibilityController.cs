using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TargetVisibilityController : UniTaskController
{
    [SerializeField] private Image ImageUI;    // クロスフェードに使用するImageオブジェクト
    [SerializeField] private Texture Texture1;    // テクスチャ1枚目
    [SerializeField] private Texture Texture2;    // テクスチャ2枚目
    [SerializeField] private Texture Texture3;    //テクスチャ3枚目
    private bool lockVisibilityLevel1;
    private bool lockVisibilityLevel2;
    private CancellationToken token;
    private float alpha;

    private void Start()
    {
        InitializeShader(Texture1, Texture2);
        lockVisibilityLevel1 = false;
        lockVisibilityLevel2 = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LimitVisibilityManager(GameManager.Instance.numSubShutter);
        }
    }

    /// <summary>
    /// ターゲットの視界を撮影した枚数に応じて変化させる
    /// </summary>
    /// <param name="subCount">異質なものを撮影した枚数</param>
    public void LimitVisibilityManager(int subCount)
    {
        if(subCount == Config.targetVisibilityFirstPhase && !lockVisibilityLevel1)
        {
            //クロスフェード実行
            //BlendManager(Texture1, Texture2, token).Forget();
            UniTaskUpdate(() => SetTexture(Texture1, Texture2, ImageUI.material), () => UpdateUniTask(ImageUI.material), () => { return (alpha >= 1.0f); }, token, UniTaskCancellMode.Auto).Forget();
            lockVisibilityLevel1 = true;
        }
        else if(subCount == Config.targetVisibilitySecondPhase && !lockVisibilityLevel2)
        {
            //クロスフェード実行
            //BlendManager(Texture2, Texture3, token).Forget();
            UniTaskUpdate(() => SetTexture(Texture2, Texture3, ImageUI.material), () => UpdateUniTask(ImageUI.material), () => { return (alpha >= 1.0f); }, token, UniTaskCancellMode.Auto).Forget();
            lockVisibilityLevel2 = true;
        }
    }

    /// <summary>
    /// UniTaskControllerのUpdate関数
    /// </summary>
    public void UpdateUniTask(Material material)
    {
        Debug.Log("視界update");
        alpha += Time.deltaTime;

        material.SetFloat("_Blend", alpha);
    }

    /// <summary>
    /// マテリアルにテクスチャを設定する
    /// </summary>
    /// <param name="before"></param>
    /// <param name="after"></param>
    private void SetTexture(Texture before, Texture after, Material material)
    {
        Debug.Log("視界start");
        alpha = 0;
        material.SetTexture("_Texture1", before);
        material.SetTexture("_Texture2", after);
    }

    /// <summary>
    /// シェーダーを初期化する関数
    /// </summary>
    /// <param name="texture1"></param>
    /// <param name="texture2"></param>
    private void InitializeShader(Texture texture1, Texture texture2)
    {
        Material material = ImageUI.material;
        material.SetFloat("_Blend", 0);
        material.SetTexture("_Texture1", texture1);
        material.SetTexture("_Texture2", texture2);
    }
}
