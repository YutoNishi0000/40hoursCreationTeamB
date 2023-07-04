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
        _Direction = Shader.PropertyToID("_Direction"); //�v���p�e�BID���擾
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var finalTexture = RenderTexture.GetTemporary(src.width, src.height);
        var mixTexture = RenderTexture.GetTemporary(src.width, src.height);
        //�����𔼕��ɂ��������_�[�e�X�N�`�����쐬�i�܂��A�Ȃɂ��`����Ă��Ȃ��j
        var rth = RenderTexture.GetTemporary(src.width / 2, src.height);

        var h = new Vector2(1, 0); //�u���[�����̃x�N�g��(U����)
        gaussianBlurMaterial.SetVector(_Direction, h); //�V�F�[�_�[���̕ϐ��Ƀu���[������ݒ�

        Graphics.Blit(src, rth, gaussianBlurMaterial);

        //��̃e�N�X�`���T�C�Y����A����ɏc�����ɂ��������_�[�e�X�N�`�����쐬�i�܂��A�Ȃɂ��`����Ă��Ȃ��j
        var rtv = RenderTexture.GetTemporary(rth.width, rth.height / 2);

        var v = new Vector2(0, 1);�@//�u���[�����̃x�N�g��(V����)        
        gaussianBlurMaterial.SetVector(_Direction, v); //�V�F�[�_�[���̕ϐ��Ƀu���[������ݒ�

        Graphics.Blit(rth, rtv, gaussianBlurMaterial); // �u���[�������s��

        Graphics.Blit(rtv, finalTexture, gaussianBlurMaterial); //���T�C�Y����1/4�ɂȂ��������_�[�e�N�X�`�����A���̃T�C�Y�ɖ߂�

        mixMaterial.SetTexture("_Texture1", finalTexture);
        mixMaterial.SetTexture("_Texture2", src);
        Graphics.Blit(finalTexture, dest, mixMaterial);

        RenderTexture.ReleaseTemporary(rtv); //�e���|���������_�[�e�X�N�`���̊J��
        RenderTexture.ReleaseTemporary(rth); //�J�����Ȃ��ƃ��������[�N����̂Œ���
        RenderTexture.ReleaseTemporary(finalTexture); //�J�����Ȃ��ƃ��������[�N����̂Œ���
        RenderTexture.ReleaseTemporary(mixTexture); //�J�����Ȃ��ƃ��������[�N����̂Œ���
    }
}