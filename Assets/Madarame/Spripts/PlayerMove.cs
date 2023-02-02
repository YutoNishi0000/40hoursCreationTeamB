using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�v���C���[�̈ړ����x
    float speed = 5.0f;

    //�J�����I�u�W�F�N�g
    public GameObject mainCamera;

    //z���𒲐�
    public int zAddjust = 5;
    
    //x���𒲐�
    public int xAddjust = 3;

    void Update()
    {
        //�J�����̓v���C���[�Ɠ����ʒu�ɂ���
        mainCamera.transform.position = new Vector3(transform.position.x + xAddjust, transform.position.y, transform.position.z - zAddjust);

        // W�L�[�i�O���ړ��j
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += speed * transform.forward * Time.deltaTime;
        }

        // S�L�[�i����ړ��j
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= speed * transform.forward * Time.deltaTime;
        }

        // D�L�[�i�E�ړ��j
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += speed * transform.right * Time.deltaTime;
        }

        // A�L�[�i���ړ��j
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= speed * transform.right * Time.deltaTime;
        }
    }
}
