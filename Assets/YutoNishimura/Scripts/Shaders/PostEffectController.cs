using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ポスト　エフェクトを管理するクラス
//カメラオブジェクトにアタッチ
public class PostEffectController : MonoBehaviour
{
    [SerializeField] private Material effectMaterial;

    private static bool postEffectFlag;

    private void Start()
    {
        postEffectFlag = true;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // ポストエフェクトをかけない場合
        if (effectMaterial == null || !postEffectFlag)
        {
            Graphics.Blit(src, dest);
            return;
        }
        else if (postEffectFlag)
        {
            // ポストエフェクトをかける場合
            Graphics.Blit(src, dest, effectMaterial);
        }
    }

    /// <summary>
    /// ポストエフェクトを実行するかのフラグをセットするための関数
    /// </summary>
    /// <param name="flag"></param>
    public static void SetPostEffectFlag(bool flag)
    {
        postEffectFlag = flag;
    }
}
