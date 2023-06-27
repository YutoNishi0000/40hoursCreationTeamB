using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�|�X�g�@�G�t�F�N�g���Ǘ�����N���X
//�J�����I�u�W�F�N�g�ɃA�^�b�`
public class PostEffectController : MonoBehaviour
{
    [SerializeField] private Material effectMaterial;

    private static bool postEffectFlag;

    private void Start()
    {
        postEffectFlag = true;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // �|�X�g�G�t�F�N�g�������Ȃ��ꍇ
        if (effectMaterial == null || !postEffectFlag)
        {
            Graphics.Blit(src, dest);
            return;
        }
        else if (postEffectFlag)
        {
            // �|�X�g�G�t�F�N�g��������ꍇ
            Graphics.Blit(src, dest, effectMaterial);
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
}
