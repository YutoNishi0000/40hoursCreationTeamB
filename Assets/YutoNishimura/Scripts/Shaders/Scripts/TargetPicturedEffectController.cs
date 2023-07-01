using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TargetPicturedEffectController : MonoBehaviour
{
    //円状に拡散するシェーダを持つマテリアル
    [SerializeField] private Material diffusionCircleMaterial;
    [SerializeField] private float effectTime = 2;
    [SerializeField] private float fadeTime = 1;
    private float totalPrevTime;
    private CancellationToken token;

    private void Awake()
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
            Debug.Log("ああああああああああああああああああああああああああああああああああああああああああ");
            RespawnTargetEffect(diffusionCircleMaterial).Forget();
        }
    }

    private async UniTask RespawnTargetEffect(Material material)
    {
        float trigger = 0;   //ポストエフェクトを発動するための値

        SetCancelToken(ref token);

        while(true)
        {
            trigger += Time.deltaTime;

            if(trigger >= fadeTime)
            {
                material.SetFloat("_Trigger", (totalPrevTime - trigger));
                if((totalPrevTime - trigger) <= 0)
                {
                    break;
                }
            }
            else
            {
                material.SetFloat("_Trigger", trigger);
            }

            await UniTask.DelayFrame(1, PlayerLoopTiming.Update, token);
        }
    }

    private void SetCancelToken(ref CancellationToken token)
    {
        //毎回新しいインスタンスを生成しないようにするため参照型を引数として受け取っている
        //メリット：キャンセルトークンをGetSetする必要がなくなる

        //自身が破棄されるときにUnitaskを中止するためのキャンセルトークンを取得
        token = this.GetCancellationTokenOnDestroy();
    }
}
