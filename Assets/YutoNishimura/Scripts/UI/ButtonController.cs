using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;


public class ButtonController : UIController
{
    [Header("���b�Ń|�b�v�A�b�v�A�|�b�v�_�E���A�o�[���ړ����邩")]
    [SerializeField] protected float changeScaleTime = 0.1f;
    [Header("�|�b�v�A�b�v��̑傫���i�{���j")]
    [SerializeField] protected float afterScale = 1.5f;
    [Header("�|�b�v�A�b�v�O�̃X�v���C�g")]
    [SerializeField] protected Sprite initialImage;
    [Header("�|�b�v�A�b�v��̃{�^���̃C���[�W")]
    [SerializeField] protected Sprite afterImage;

    //���������Ŏg���ϐ�
    protected Image tempButton;
    private readonly Vector3 normalVec = new Vector3(1, 1, 1);
    

    private void Start()
    {
        InitializeButton();
    }
    
    //�p�����Start�֐��A�܂���Awake�֐����ŕK���Ăяo���Ȃ���΂Ȃ�Ȃ��֐�
    protected void InitializeButton()
    {
        tempButton = this.GetComponent<Image>();
    }
    public void PopUp()
    {
        tempButton.sprite = afterImage;
        tempButton.rectTransform.DOScale(normalVec * afterScale, changeScaleTime);
    }

    public void PopDown()
    {
        tempButton.sprite = initialImage;
        tempButton.rectTransform.DOScale(normalVec, changeScaleTime);
    }
}
