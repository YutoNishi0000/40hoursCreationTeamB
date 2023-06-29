using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPicturedEffectController : MonoBehaviour
{
    //�~��Ɋg�U����V�F�[�_�����}�e���A��
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
