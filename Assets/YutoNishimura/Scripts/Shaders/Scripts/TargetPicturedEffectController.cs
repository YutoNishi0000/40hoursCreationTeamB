using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TargetPicturedEffectController : UniTaskController
{
    [SerializeField] private Material diffusionCircleMaterial;    //�~��Ɋg�U����V�F�[�_�����}�e���A��
    [SerializeField] private float effectTime = 1;                //�|�X�g�G�t�F�N�g��\�����鎞��
    [SerializeField] private float fadeTime = 0.5f;               //�t�F�[�h����
    private float totalPrevTime;                                  //�|�X�g�G�t�F�N�g���\������Ă��������܂ł̑�����
    private CancellationToken token;                              //�L�����Z���g�[�N��
    private float trigger;                                        //�|�X�g�G�t�F�N�g�������邽�߂̒l

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
            UniTaskUpdate(() => StartUniTask(), () => UpdateUniTask(diffusionCircleMaterial, fadeTime), () => { return ((totalPrevTime - trigger) <= 0); }, token, UniTaskCancellMode.Auto).Forget();
        }
    }

    /// <summary>
    /// UniTaskUpdate���Ă΂ꂽ�t���Ɏ��s�����
    /// </summary>
    public override void StartUniTask()
    {
        trigger = 0;
    }

    /// <summary>
    /// UniTaskController���Ŗ��t���[���Ăяo�����
    /// </summary>
    /// <param name="material">�}�e���A��</param>
    /// <param name="fadeTime">�t�F�[�h����</param>
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
