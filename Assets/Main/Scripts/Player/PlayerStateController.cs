using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̏�Ԃɉ����ăX�e�[�g���Ǘ�����
public class PlayerStateController : MonoBehaviour
{
    public enum PlayerState
    {
        Move,                    //�ړ����
        ViewportLocked,          //���_���Œ肳��Ă�����
        Voyeurism,               //���B���
        TalkEvent                //��b�C�x���g�����������Ƃ��̏��
    }

    private PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        //�C���X�^���X����
        playerState = new PlayerState();
    }

    // Update is called once per frame
    void Update()
    {
        //if(RaycastController.Lockon)
        //{
        //    Debug.Log("�r���[�|�[�g���b�N��� ");
        //    SetPlayerState(PlayerState.ViewportLocked);
        //}
        //else if(ChangeCameraAngle._voyeurism)
        //{
        //    Debug.Log("���B���");
        //    SetPlayerState(PlayerState.Voyeurism);
        //}
        //else if(UIController._talkStart)
        //{
        //    Debug.Log("�n���J�`�C�x���g�J�n��� ");

        //    SetPlayerState(PlayerState.TalkEvent);
        //}
        //else
        //{
        //    //Debug.Log("�������� ");

        //    SetPlayerState(PlayerState.Move);
        //}
    }

    //�Z�b�^�[
    public void SetPlayerState(PlayerState state)
    {
        playerState = state;
    }

    //�Q�b�^�[
    public PlayerState GetPlayerState()
    {
        return playerState;
    }
}
