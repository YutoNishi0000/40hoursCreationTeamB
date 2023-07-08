using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

//�|�X�g�@�G�t�F�N�g���Ǘ�����N���X
//�J�����I�u�W�F�N�g�ɃA�^�b�`
public class PostEffectController : UniTaskController
{
    [SerializeField] private Material nearTargetetEffect;    //�Ώۂ��߂Â����Ƃ��ɓd���𗬂����߂̃}�e���A��
    [SerializeField] private Material nearTargetetImpact;    //�Ώۂ��߂��ɂ��鎞�ɂڂ₩�����߂̃}�e���A��
    [SerializeField] private Material gaussianBlurMaterial;  //�K�E�V�A���u���[�}�e���A��
    [SerializeField] private Material mixMaterial;           //�e�N�X�`�������p�}�e���A��
    private static bool postEffectFlag;                      //�|�X�g�G�t�F�N�g�������邩�ǂ���
    [SerializeField, Range(0, 1)] private float blendTex = 0.5f;  //�e�N�X�`�����������i�V�F�[�_�[�̕ϐ��ɃZ�b�g�j
    private float blend;                                     //�V�F�[�_�[�Ń��l�����X�ɑ傫�����Ă����Ƃ��Ɏg���ϐ�
    private CancellationToken token;                         //�L�����Z���g�[�N��
    private int _Direction;

    private void Start()
    {
        _Direction = Shader.PropertyToID("_Direction"); //�v���p�e�BID���擾
        postEffectFlag = true;
        UniTaskUpdate(() => { }, UpdateUniTask, null,  () => { return false; }, token, UniTaskCancellMode.Auto).Forget();
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
        Graphics.Blit(finalTexture, mixTexture, mixMaterial);

        // �|�X�g�G�t�F�N�g�������Ȃ��ꍇ
        if (!postEffectFlag || nearTargetetImpact == null || nearTargetetEffect == null)
        {
            Graphics.Blit(mixTexture, dest);
        }
        else if (postEffectFlag)
        {
            //�܂��ŏ��ɉ�ʂ��ڂ���
            Graphics.Blit(mixTexture, dest, nearTargetetImpact);
            //�ڂ�������|�X�g�G�t�F�N�g��������
            Graphics.Blit(mixTexture, dest, nearTargetetEffect);
        }

        RenderTexture.ReleaseTemporary(rtv); //�e���|���������_�[�e�X�N�`���̊J��
        RenderTexture.ReleaseTemporary(rth); //�J�����Ȃ��ƃ��������[�N����̂Œ���
        RenderTexture.ReleaseTemporary(finalTexture); //�J�����Ȃ��ƃ��������[�N����̂Œ���
        RenderTexture.ReleaseTemporary(mixTexture); //�J�����Ȃ��ƃ��������[�N����̂Œ���
    }

    /// <summary>
    /// �|�X�g�G�t�F�N�g�����s���邩�̃t���O���Z�b�g���邽�߂̊֐�
    /// </summary>
    /// <param name="flag">�t���O</param>
    public static void SetPostEffectFlag(bool flag)
    {
        postEffectFlag = flag;
    }

    /// <summary>
    /// ���t���[����яo�����
    /// </summary>
    public override void UpdateUniTask()
    {
        //�Ώۂ̃I�u�W�F�N�g���擾
        GameObject targetObject = RespawTarget.GetCurrentTargetObj();

        //�Ώۂ̃I�u�W�F�N�g��null����Ȃ��ăX�L���Q���������Ă�����
        if (targetObject != null && SkillManager.GetSpiritSenceFlag())
        {
            //�v���C���[�ƑΏۂ̋������擾
            float dis = Vector3.Distance(transform.position, targetObject.transform.position);

            //�|�X�g�G�t�F�N�g�Đ�
            SunderManager(dis, Config.detectionTargetDistance);
        }
        //�Ώۂ̃I�u�W�F�N�g��null��������
        else
        {
            SetPostEffectFlag(false);
        }
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
            //���l�𑝂₷
            blend += Time.deltaTime;
            //blend��1���傫��������1��Ԃ�
            nearTargetetEffect.SetFloat("_Blend", ((blend >= 1) ? 1 : blend));
            //�u���[��������
            nearTargetetImpact.SetFloat("_BlurDegree", 0.05f);
            SetPostEffectFlag(true);
        }
        else
        {
            //�S�Ẵp�����[�^�[�A�}�e���A���̒l�����Z�b�g
            blend = 0;
            nearTargetetEffect.SetFloat("_Blend", 0);
            nearTargetetImpact.SetFloat("_BlurDegree", 0);
            SetPostEffectFlag(false);
        }
    }
}
