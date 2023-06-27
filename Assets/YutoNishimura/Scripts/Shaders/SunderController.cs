using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

//�Ώۂɋ߂Â����Ƃ��A��ʂɓd���������悤�ɂ�����
//��r�I�d�������Ȃ̂�Unitask��p����
public class SunderController : Actor
{
    [SerializeField] private GameObject playerObject;

    private CancellationToken token;

    private void Start()
    {
        SunderUpdate(token).Forget();
    }

    private async UniTask SunderUpdate(CancellationToken token)
    {
        //���g���j�������Ƃ���Unitask�𒆎~���邽�߂̃L�����Z���g�[�N�����擾
        SetCancelToken(ref token);

        while (true)
        {
            GameObject targetObject = RespawTarget.GetCurrentTargetObj();

            if(targetObject == null)
            {
                return;
            }

            float dis = Vector3.Distance(transform.position, targetObject.transform.position);

            SunderManager(dis, 100);

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
        if(distance <= limit)
        {
            PostEffectController.SetPostEffectFlag(true);
        }
        else
        {
            PostEffectController.SetPostEffectFlag(false);
        }
    }

}
