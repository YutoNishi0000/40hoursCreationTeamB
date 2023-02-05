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
        Voyeurism                //���B���
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
        if(RayTest.lockon)
        {
            SetPlayerState(PlayerState.ViewportLocked);
        }
        else if(ChangeCameraAngle._voyeurism)
        {
            SetPlayerState(PlayerState.Voyeurism);
        }
        else
        {
            SetPlayerState(PlayerState.Move);
        }
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
