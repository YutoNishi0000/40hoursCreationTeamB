using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPicturedEffectController : MonoBehaviour
{
    //円状に拡散するシェーダを持つマテリアル
    [SerializeField] private Material diffusionCircleMaterial;

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
}
