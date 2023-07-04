using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//�|�X�g�@�G�t�F�N�g���Ǘ�����N���X
//�J�����I�u�W�F�N�g�ɃA�^�b�`
public class PostEffectController : UniTaskController
{
    [SerializeField] private Material nearTargetetEffect;

    private static bool postEffectFlag;

    private float blend;                      //�V�F�[�_�[�Ń��l�����X�ɑ傫�����Ă����Ƃ��Ɏg���ϐ�

    private CancellationToken token;

    [SerializeField] private Material nearTargetetImpact;

    delegate void PostEffect();

    private void Start()
    {
        postEffectFlag = true;
        //SunderUpdate(token).Forget();
        UniTaskUpdate(()=> { }, UpdateUniTask, () => { return false; }, token, UniTaskCancellMode.Auto).Forget();
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

            //�|�X�g�G�t�F�N�g�����鏇�Ԃ����Ԃ����̂悤�ɂ��邱�ƂŁA�ォ��ѓd�̃|�X�g�G�t�F�N�g��������悤�ɂȂ�
        }

        //�����ŁA�K�E�V�A���u���[��K�p�����e�N�X�`���ƌ��̃e�N�X�`���̍������s��->��C�����o������
    }

    /// <summary>
    /// ���t���[����яo�����
    /// </summary>
    public override void UpdateUniTask()
    {
        Debug.Log("�d��update");
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
    /// �|�X�g�G�t�F�N�g�����s���邩�̃t���O���Z�b�g���邽�߂̊֐�
    /// </summary>
    /// <param name="flag">�t���O</param>
    public static void SetPostEffectFlag(bool flag)
    {
        Debug.Log("�d��start");
        postEffectFlag = flag;
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
            //�S�Ă̒l�����Z�b�g
            blend = 0;
            nearTargetetEffect.SetFloat("_Blend", 0);
            nearTargetetImpact.SetFloat("_BlurDegree", 0);
            SetPostEffectFlag(false);
        }
    }
}
