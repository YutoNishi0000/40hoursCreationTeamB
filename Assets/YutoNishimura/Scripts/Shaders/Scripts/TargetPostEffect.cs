using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class TargetPostEffect : MonoBehaviour
{
    [SerializeField] private Material gaussianBlurMaterial;
    [SerializeField] private Material mixMaterial;
    private int _Direction;

    private void Start()
    {
        _Direction = Shader.PropertyToID("_Direction"); //プロパティIDを取得
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var finalTexture = RenderTexture.GetTemporary(src.width, src.height);
        var mixTexture = RenderTexture.GetTemporary(src.width, src.height);
        //横幅を半分にしたレンダーテスクチャを作成（まだ、なにも描かれていない）
        var rth = RenderTexture.GetTemporary(src.width / 2, src.height);

        var h = new Vector2(1, 0); //ブラー方向のベクトル(U方向)
        gaussianBlurMaterial.SetVector(_Direction, h); //シェーダー内の変数にブラー方向を設定

        Graphics.Blit(src, rth, gaussianBlurMaterial);

        //先のテクスチャサイズから、さらに縦半分にしたレンダーテスクチャを作成（まだ、なにも描かれていない）
        var rtv = RenderTexture.GetTemporary(rth.width, rth.height / 2);

        var v = new Vector2(0, 1);　//ブラー方向のベクトル(V方向)        
        gaussianBlurMaterial.SetVector(_Direction, v); //シェーダー内の変数にブラー方向を設定

        Graphics.Blit(rth, rtv, gaussianBlurMaterial); // ブラー処理を行う

        Graphics.Blit(rtv, finalTexture, gaussianBlurMaterial); //元サイズから1/4になったレンダーテクスチャを、元のサイズに戻す

        mixMaterial.SetTexture("_Texture1", finalTexture);
        mixMaterial.SetTexture("_Texture2", src);
        Graphics.Blit(finalTexture, dest, mixMaterial);

        RenderTexture.ReleaseTemporary(rtv); //テンポラリレンダーテスクチャの開放
        RenderTexture.ReleaseTemporary(rth); //開放しないとメモリリークするので注意
        RenderTexture.ReleaseTemporary(finalTexture); //開放しないとメモリリークするので注意
        RenderTexture.ReleaseTemporary(mixTexture); //開放しないとメモリリークするので注意
    }
}