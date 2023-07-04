using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TargetVisibilityController : UniTaskController
{
    [SerializeField] private Image ImageUI;       // �N���X�t�F�[�h�Ɏg�p����Image�I�u�W�F�N�g
    [SerializeField] private Texture Texture1;    // �e�N�X�`��1����
    [SerializeField] private Texture Texture2;    // �e�N�X�`��2����
    [SerializeField] private Texture Texture3;    //�e�N�X�`��3����
    private bool lockVisibilityLevel1;            //���ڂ̃N���X�t�F�[�h�������鎞�Ɏg���t���O
    private bool lockVisibilityLevel2;            //���ڂ̃N���X�t�F�[�h�������鎞�Ɏg���t���O
    private CancellationToken token;              //�L�����Z���g�[�N��
    private float alpha;                          //�|�X�g�G�t�F�N�g�̃A���t�@�l

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
        if(subCount == Config.targetVisibilityFirstPhase && !lockVisibilityLevel1)
        {
            //�N���X�t�F�[�h���s
            //BlendManager(Texture1, Texture2, token).Forget();
            UniTaskUpdate(() => SetTexture(Texture1, Texture2, ImageUI.material), () => UpdateUniTask(ImageUI.material), () => { return (alpha >= 1.0f); }, token, UniTaskCancellMode.Auto).Forget();
            lockVisibilityLevel1 = true;
        }
        else if(subCount == Config.targetVisibilitySecondPhase && !lockVisibilityLevel2)
        {
            //�N���X�t�F�[�h���s
            //BlendManager(Texture2, Texture3, token).Forget();
            UniTaskUpdate(() => SetTexture(Texture2, Texture3, ImageUI.material), () => UpdateUniTask(ImageUI.material), () => { return (alpha >= 1.0f); }, token, UniTaskCancellMode.Auto).Forget();
            lockVisibilityLevel2 = true;
        }
    }

    /// <summary>
    /// UniTaskController��Update�֐�
    /// </summary>
    /// <param name="material">�����������}�e���A��</param>
    public void UpdateUniTask(Material material)
    {
        alpha += Time.deltaTime;

        material.SetFloat("_Blend", alpha);
    }

    /// <summary>
    /// �}�e���A���Ƀe�N�X�`����ݒ肷��
    /// </summary>
    /// <param name="before">�ύX�O�̃e�N�X�`��</param>
    /// <param name="after">�ύX��̃e�N�X�`��</param>
    private void SetTexture(Texture before, Texture after, Material material)
    {
        alpha = 0;
        material.SetTexture("_Texture1", before);
        material.SetTexture("_Texture2", after);
    }

    /// <summary>
    /// �V�F�[�_�[������������֐��i�i�v�I�ɕύX���ۑ�����Ă��܂����߁A�����ŏ����������Ă����j
    /// </summary>
    /// <param name="texture1">�ŏ��ɕ\���������e�N�X�`��</param>
    /// <param name="texture2">2�Ԗڂɕ\���������e�N�X�`��</param>
    private void InitializeShader(Texture texture1, Texture texture2)
    {
        Material material = ImageUI.material;
        material.SetFloat("_Blend", 0);
        material.SetTexture("_Texture1", texture1);
        material.SetTexture("_Texture2", texture2);
    }
}
