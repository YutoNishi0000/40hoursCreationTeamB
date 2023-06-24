using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TargetVisibilityController : MonoBehaviour
{
    [SerializeField] private Image ImageUI;    // �N���X�t�F�[�h�Ɏg�p����Image�I�u�W�F�N�g
    [SerializeField] private Texture Texture1;    // �e�N�X�`��1����
    [SerializeField] private Texture Texture2;    // �e�N�X�`��2����
    [SerializeField] private Texture Texture3;    //�e�N�X�`��3����
    private bool lockVisibilityLevel1;
    private bool lockVisibilityLevel2;

    private void Start()
    {
        InitializeShader(Texture1, Texture2);
        lockVisibilityLevel1 = false;
        lockVisibilityLevel2 = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LimitVisibilityManager(GameManager.Instance.numSubShutter);
        }
    }

    /// <summary>
    /// �^�[�Q�b�g�̎��E���B�e���������ɉ����ĕω�������
    /// </summary>
    /// <param name="subCount">�َ��Ȃ��̂��B�e��������</param>
    public void LimitVisibilityManager(int subCount)
    {
        switch (subCount)
        {
            case 1:
                if(lockVisibilityLevel1)
                {
                    return;
                }

                BlendManager(Texture1, Texture2).Forget();
                lockVisibilityLevel1 = true;
                break;
            case 2:
                if (lockVisibilityLevel2)
                {
                    return;
                }

                BlendManager(Texture2, Texture3).Forget();
                lockVisibilityLevel2 = true;
                break;
        }
    }

    /// <summary>
    /// �e�N�X�`���̃u�����h���s��
    /// </summary>
    /// <param name="before"></param>
    /// <param name="after"></param>
    /// <returns></returns>
    private async UniTask BlendManager(Texture before, Texture after)
    { 
        float alpha = 0.0f;
        // Image�̃}�e���A�����擾
        Material material = ImageUI.material;
        material.SetTexture("_Texture1", before);
        material.SetTexture("_Texture2", after);

        while (true)
        {
            alpha += Time.deltaTime;

            material.SetFloat("_Blend", alpha);

            if(alpha >= 1.0f)
            {
                //���[�v���甲���o��
                break;
            }

            //1�t���[���҂�
            await UniTask.DelayFrame(1);
        }
    }

    /// <summary>
    /// �V�F�[�_�[������������֐�
    /// </summary>
    /// <param name="texture1"></param>
    /// <param name="texture2"></param>
    private void InitializeShader(Texture texture1, Texture texture2)
    {
        Material material = ImageUI.material;
        material.SetFloat("_Blend", 0);
        material.SetTexture("_Texture1", texture1);
        material.SetTexture("_Texture2", texture2);
    }
}
