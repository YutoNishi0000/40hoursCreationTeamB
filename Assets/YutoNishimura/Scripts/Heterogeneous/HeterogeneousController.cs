using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�َ��Ȃ��̎��g�ɃA�^�b�`�������
public class HeterogeneousController : Actor
{
    private Material material;
    private readonly float destroyTime = 1;
    private float color_a;
    public bool takenPicFlag;       //�ʐ^���B��ꂽ���ǂ���

    void Start()
    {
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
            Vector3 strangeObjVec = transform.position - playerInstance.transform.position;
            Vector3 playerForwardVec = playerInstance.transform.forward;

            float angle = Vector3.Angle(playerForwardVec, strangeObjVec);

            //���苗��(��ŏ�Ɉړ�������)
            const float enableSeeDis = 7.0f;

            float judgeDis = strangeObjVec.magnitude * Mathf.Cos((angle / 360) * Mathf.PI * 2);

            if (judgeDis <= enableSeeDis && GameManager.Instance.IsSubPhoto)
            {
                //�َ��Ȃ��̂��B�����񐔂̃J�E���g���C���N�������g�{�X�R�A��+10����
                GameManager.Instance.numSubShutter++;
                ScoreManger.Score += 10;

                if(GameManager.Instance.numSubShutter == 1)
                {
                    //�^�C���J�E���g�J�n
                    GameManager.Instance.skillManager.StartCount();
                }
                else if(GameManager.Instance.numSubShutter == 3)
                {
                    //�X�L������
                    GameManager.Instance.skillManager.SkillImposition();

                    GameManager.Instance.numSubShutter = 0;
                }

                //�O�̂��߂����ł��t���O�̓I�t�ɂ��Ă���
                GameManager.Instance.IsSubPhoto = false;

                //���M�����ł�����t���O���I����
                takenPicFlag = true;
            }

            //�ꉞ�����Ńt���O�̓I�t�ɂ��Ă���
            GameManager.Instance.IsSubPhoto = false;
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

        //�A���t�@�l���O�ȉ��ɂȂ����玩�g���폜
        if (color_a < 0)
        {
            Destroy(gameObject);
        }

        color_a -= Time.deltaTime;

        GetComponent<Renderer>().material.color -= new Color(0, 0, 0, color_a);
    }
}