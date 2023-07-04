using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TargetPicturedEffectController : UniTaskController
{
    [SerializeField] private Material diffusionCircleMaterial;    //円状に拡散するシェーダを持つマテリアル
    [SerializeField] private float effectTime = 1;                //ポストエフェクトを表示する時間
    [SerializeField] private float fadeTime = 0.5f;               //フェード時間
    private float totalPrevTime;                                  //ポストエフェクトが表示されてから消えるまでの総時間
    private CancellationToken token;                              //キャンセルトークン
    private float trigger;                                        //ポストエフェクトをかけるための値

    private void Start()
    {
        totalPrevTime = effectTime + fadeTime * 2;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(diffusionCircleMaterial ==  null)
        {
            Graphics.Blit(source, destination);
        }
        else
        {
            Graphics.Blit(source, destination, diffusionCircleMaterial);
        }
    }

    private void Update()
    {
        //ここはリスポーンの際UniTaskを使って、１フレームしか呼ばれないようになっているため、ここでリスポーンエフェクトを発動させる
        if(RespawTarget.GetCurrentTargetObj() == null)
        {
            UniTaskUpdate(() => StartUniTask(), () => UpdateUniTask(diffusionCircleMaterial, fadeTime), () => { return ((totalPrevTime - trigger) <= 0); }, token, UniTaskCancellMode.Auto).Forget();
        }
    }

    /// <summary>
    /// UniTaskUpdateが呼ばれた春寒に実行される
    /// </summary>
    public override void StartUniTask()
    {
        trigger = 0;
    }

    /// <summary>
    /// UniTaskController内で毎フレーム呼び出される
    /// </summary>
    /// <param name="material">マテリアル</param>
    /// <param name="fadeTime">フェード時間</param>
    public void UpdateUniTask(Material material, float fadeTime)
    {
        trigger += Time.deltaTime;

        if (trigger >= fadeTime)
        {
            material.SetFloat("_Trigger", (totalPrevTime - trigger));
        }
        else
        {
            material.SetFloat("_Trigger", trigger);
        }
    }
}
