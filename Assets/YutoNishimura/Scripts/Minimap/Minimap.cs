using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�v���C���[�ƃ^�[�Q�b�g�̈ʒu��c��
public class Minimap : Actor
{
    [SerializeField] private Vector2 fieldParam;         //�t�B�[���h�̕��ƍ���
    [SerializeField] private Vector2 minimapParam;       //UI��ŕ\������UI�̑傫�����w��
    [SerializeField] private GameObject fieldObj;        //�t�B�[���h�̐e�I�u�W�F�N�g���擾
    [SerializeField] private Image basePos;              //�~�j�}�b�v��\�������_�i�~�j�}�b�v�̃e�N�X�`����}���j
    [SerializeField] private Image player;               //�v���C���[�̃e�N�X�`��
    [SerializeField] private Image target;               //�Ώۂ̃e�N�X�`��

    //����
    //�v���C���[�ƑΏۂ̂�x���W�Az���W�����ꂼ�ꋁ�߃t�B�[���h�̕��A�����ł��ꂼ�ꊄ���-1����1�͈̔͂ŕ\�����邱�Ƃ��ł���̂ł���𗘗p����

    void Start()
    {

    }

    void Update()
    {
        //�v���C���[�ƑΏۂ̈ʒu����ɍX�V���ĕ\������
        player.rectTransform.position = GetMinimapPos(playerInstance.transform.position);
        target.rectTransform.position = GetMinimapPos(targetInstance.transform.position);
    }

    //UI��ł̃v���C���[�ƑΏۂ̈ʒu���擾
    private Vector2 GetMinimapPos(Vector3 pos)
    {
        //-1����1�͈̔͂Ɏ��k���AUI�悤�ɑ傫�������킹��
        return (new Vector2(pos.x - fieldObj.transform.position.x, pos.z - fieldObj.transform.position.z) / fieldParam) * minimapParam + (Vector2)basePos.rectTransform.position;
    }
}
