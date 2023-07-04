using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TargetPicturedEffectController : UniTaskController
{
    //�~��Ɋg�U����V�F�[�_�����}�e���A��
    [SerializeField] private Material diffusionCircleMaterial;
    [SerializeField] private float effectTime = 1;
    [SerializeField] private float fadeTime = 0.5f;
    private float totalPrevTime;
    private CancellationToken token;
    private float trigger;

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
        //�����̓��X�|�[���̍�UniTask���g���āA�P�t���[�������Ă΂�Ȃ��悤�ɂȂ��Ă��邽�߁A�����Ń��X�|�[���G�t�F�N�g�𔭓�������
        if(RespawTarget.GetCurrentTargetObj() == null)
        {
            //RespawnTargetEffect(diffusionCircleMaterial).Forget();
            UniTaskUpdate(() => StartUniTask(), () => UpdateUniTask(diffusionCircleMaterial, fadeTime), () => { return ((totalPrevTime - trigger) <= 0); }, token, UniTaskCancellMode.Auto).Forget();
        }
    }

    public override void StartUniTask()
    {
        Debug.Log("�Ώێ��Estart");
        trigger = 0;
    }

    /// <summary>
    /// UniTaskController���Ŗ��t���[���Ăяo�����
    /// </summary>
    /// <param name="material"></param>
    /// <param name="fadeTime"></param>
    public void UpdateUniTask(Material material, float fadeTime)
    {
        Debug.Log("�Ώێ��Eupdate");
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

    //private async UniTask RespawnTargetEffect(Material material)
    //{
    //    float trigger = 0;   //�|�X�g�G�t�F�N�g�𔭓����邽�߂̒l

    //    SetCancelToken(ref token);

    //    while(true)
    //    {
    //        trigger += Time.deltaTime;

    //        if(trigger >= fadeTime)
    //        {
    //            material.SetFloat("_Trigger", (totalPrevTime - trigger));
    //            if((totalPrevTime - trigger) <= 0)
    //            {
    //                break;
    //            }
    //        }
    //        else
    //        {
    //            material.SetFloat("_Trigger", trigger);
    //        }

    //        await UniTask.DelayFrame(1, PlayerLoopTiming.Update, token);
    //    }
    //}

    //private void SetCancelToken(ref CancellationToken token)
    //{
    //    //����V�����C���X�^���X�𐶐����Ȃ��悤�ɂ��邽�ߎQ�ƌ^�������Ƃ��Ď󂯎���Ă���
    //    //�����b�g�F�L�����Z���g�[�N����GetSet����K�v���Ȃ��Ȃ�

    //    //���g���j�������Ƃ���Unitask�𒆎~���邽�߂̃L�����Z���g�[�N�����擾
    //    token = this.GetCancellationTokenOnDestroy();
    //}
}
