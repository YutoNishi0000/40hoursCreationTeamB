using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�َ��Ȃ��̎��g�ɃA�^�b�`�������
public class HeterogeneousController : Actor
{
    private Material material;
    private readonly float destroyTime = 10;
    private float color_a;
    public bool takenPicFlag;       //�ʐ^���B��ꂽ���ǂ���
    public bool enableTakePicFlag;  //�T�u�J�����Ŏʐ^���B�邱�Ƃ��\���ǂ�����\���t���O

    void Start()
    {
        enableTakePicFlag = false;
        takenPicFlag = false;
        material = GetComponent<Material>();
        color_a = destroyTime;
    }

    void Update()
    {
        DestroyHeterogeneous();
    }

    //���g���J�����Ɏʂ��Ă����ꍇ�ɂ����Ăяo�����
    void OnWillRenderObject()
    {
        //���C���J�������猩�����Ƃ������������s��
        if (Camera.current.name == "Main Camera")
        {
            Debug.Log("�T�u�J�����������s���Ă��܂�");

            Vector3 strangeObjVec = transform.position - playerInstance.transform.position;
            Vector3 playerForwardVec = playerInstance.transform.forward;

            float angle = Vector3.Angle(playerForwardVec, strangeObjVec);

            //���苗��(��ŏ�Ɉړ�������)
            const float enableSeeDis = 7.0f;

            float judgeDis = strangeObjVec.magnitude * Mathf.Cos((angle / 360) * Mathf.PI * 2);

            if (judgeDis <= enableSeeDis)
            {
                enableTakePicFlag = true;
            }
            else
            {
                enableTakePicFlag = false;
            }
        }
    }

    //�����Ƃ��A���l�����������Ȃ�����ł�����
    //�t���O��p���Ă��̊֐����Ăяo���΂悢
    private void DestroyHeterogeneous()
    {
        if(!takenPicFlag)
        {
            return;
        }

        Debug.Log("�َ��Ȃ��̍폜");

        //�A���t�@�l���O�ȉ��ɂȂ����玩�g���폜
        if (color_a < 0)
        {
            Destroy(gameObject);
        }

        color_a -= Time.deltaTime;

        GetComponent<Renderer>().material.color -= new Color(0, 0, 0, color_a);
    }

    public void SetEnableTakePicFlag(bool flag) { enableTakePicFlag = flag; }

    public bool GetEnableTakePicFlag() { return enableTakePicFlag; }

    public void SetTakenPicFlag(bool flag) { takenPicFlag = flag; }

    public bool GetTakenPicFlag() { return takenPicFlag; }
}