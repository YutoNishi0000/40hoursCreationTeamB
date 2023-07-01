using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TargetPicturedEffectController : MonoBehaviour
{
    //�~��Ɋg�U����V�F�[�_�����}�e���A��
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
        //�����̓��X�|�[���̍�UniTask���g���āA�P�t���[�������Ă΂�Ȃ��悤�ɂȂ��Ă��邽�߁A�����Ń��X�|�[���G�t�F�N�g�𔭓�������
        if(RespawTarget.GetCurrentTargetObj() == null)
        {
            Debug.Log("������������������������������������������������������������������������������������");
            RespawnTargetEffect(diffusionCircleMaterial).Forget();
        }
    }

    private async UniTask RespawnTargetEffect(Material material)
    {
        float trigger = 0;   //�|�X�g�G�t�F�N�g�𔭓����邽�߂̒l

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
        //����V�����C���X�^���X�𐶐����Ȃ��悤�ɂ��邽�ߎQ�ƌ^�������Ƃ��Ď󂯎���Ă���
        //�����b�g�F�L�����Z���g�[�N����GetSet����K�v���Ȃ��Ȃ�

        //���g���j�������Ƃ���Unitask�𒆎~���邽�߂̃L�����Z���g�[�N�����擾
        token = this.GetCancellationTokenOnDestroy();
    }
}
