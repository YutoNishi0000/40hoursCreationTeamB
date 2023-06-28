using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//�|�X�g�@�G�t�F�N�g���Ǘ�����N���X
//�J�����I�u�W�F�N�g�ɃA�^�b�`
public class PostEffectController : MonoBehaviour
{
    [SerializeField] private Material nearTargetetEffect;

    private static bool postEffectFlag;

    private float blend;                      //�V�F�[�_�[�Ń��l�����X�ɑ傫�����Ă����Ƃ��Ɏg���ϐ�

    private CancellationToken token;

    [SerializeField] private Material nearTargetetImpact;
    [SerializeField, Range(4, 16)] private int _sampleCount = 8;
    [SerializeField, Range(0.0f, 1.0f)] private float _strength = 0.5f;

    delegate void PostEffect();

    private void Start()
    {
        postEffectFlag = true;
        SunderUpdate(token).Forget();
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // �|�X�g�G�t�F�N�g�������Ȃ��ꍇ
        if(!postEffectFlag || nearTargetetImpact == null || nearTargetetEffect == null)
        {
            Graphics.Blit(src, dest);
            return;
        }
        else if(postEffectFlag)
        {
            //�܂��ŏ��ɉ�ʂ��ڂ���
            Graphics.Blit(src, dest, nearTargetetImpact);
            //�ڂ�������|�X�g�G�t�F�N�g��������
            Graphics.Blit(src, dest, nearTargetetEffect);
        }
    }

    /// <summary>
    /// �|�X�g�G�t�F�N�g�����s���邩�̃t���O���Z�b�g���邽�߂̊֐�
    /// </summary>
    /// <param name="flag"></param>
    public static void SetPostEffectFlag(bool flag)
    {
        postEffectFlag = flag;
    }


    //��r�I�d�������Ȃ̂Ŕ񓯊�������p���ď����ł��y������
    private async UniTask SunderUpdate(CancellationToken token)
    {
        //���g���j�������Ƃ���Unitask�𒆎~���邽�߂̃L�����Z���g�[�N�����擾
        SetCancelToken(ref token);

        while (true)
        {
            GameObject targetObject = RespawTarget.GetCurrentTargetObj();

            if (targetObject != null)
            {
                float dis = Vector3.Distance(transform.position, targetObject.transform.position);

                SunderManager(dis, 20);
            }
            else
            {
                SetPostEffectFlag(false);
            }

            //1�t���[���҂�
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

    /// <summary>
    /// ��ʂɓd���𗬂����߂̊֐�
    /// </summary>
    /// <param name="distance">�v���C���[�ƑΏۂ̋���</param>
    /// <param name="limit">�ǂꂮ�炢�߂Â�����d���𗬂���</param>
    private void SunderManager(float distance, float limit)
    {
        if (distance <= limit)
        {
            blend += Time.deltaTime;
            nearTargetetEffect.SetFloat("_Blend", ((blend >= 1) ? 1 : blend));
            nearTargetetImpact.SetFloat("_BlurDegree", 0.05f);
            SetPostEffectFlag(true);
        }
        else
        {
            blend = 0;
            nearTargetetEffect.SetFloat("_Blend", 0);
            nearTargetetImpact.SetFloat("_BlurDegree", 0);
            SetPostEffectFlag(false);
        }
    }
}
