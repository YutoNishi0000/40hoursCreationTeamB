using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�َ��Ȃ��̎��g�ɃA�^�b�`�������
[RequireComponent(typeof(MeshRenderer))]
public class HeterogeneousController : Actor
{
    private Material material;
    private readonly float destroyTime = 1.0f;
    private float dethtime;
    public bool takenPicFlag;       //�ʐ^���B��ꂽ���ǂ���
    public bool enableTakePicFlag;  //�T�u�J�����Ŏʐ^���B�邱�Ƃ��\���ǂ�����\���t���O

    void Start()
    {
        enableTakePicFlag = false;
        takenPicFlag = false;
        material = GetComponent<Material>();
        dethtime = destroyTime;
    }

    void Update()
    {
        DestroyHeterogeneous();
    }

    //�t���O��p���Ă��̊֐����Ăяo���΂悢
    private void DestroyHeterogeneous()
    {
        if(!takenPicFlag)
        {
            return;
        }

        //�A���t�@�l���O�ȉ��ɂȂ����玩�g���폜
        if (dethtime < 0)
        {
            //�t�B�[���h�ɑ���Ȃ��َ��Ȃ��̂�₤�Ƃ��ɃN�[���^�C������
            HeterogeneousSetter.CoolTime();
            //�����Ɏ��g�̃Q�[���I�u�W�F�N�g���A�N�e�B�u��
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        dethtime -= Time.deltaTime;
    }

    public void SetEnableTakePicFlag(bool flag) { enableTakePicFlag = flag; }

    public bool GetEnableTakePicFlag() { return enableTakePicFlag; }

    public void SetTakenPicFlag(bool flag) { takenPicFlag = flag; }

    public bool GetTakenPicFlag() { return takenPicFlag; }
}