using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : Human
{
    Vector3 moveDirection = Vector3.zero;
    Vector3 targetDirection;        //�ړ���������̃x�N�g��
    private CharacterController controller;
    float x, z;
    float speed = 3f;

    public GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 3f, Ysensityvity = 3f;

    bool cursorLock = true;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX = -40f, maxX = 40f;

    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateControl();
    }

    private void FixedUpdate()
    {
        MoveControl();
    }

    public override void MoveControl()
    {
        //�L�[�{�[�h���͂��擾
        float v = Input.GetAxisRaw("Vertical");         //InputManager�́����̓���       
        float h = Input.GetAxisRaw("Horizontal");       //InputManager�́����̓��� 

        //�J�����̐��ʕ����x�N�g������Y�����������A���K�����ăL����������������擾
        Vector3 forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Camera.main.transform.right; //�J�����̉E�������擾

        //�J�����̕������l�������L�����̐i�s�������v�Z
        targetDirection = h * right + v * forward;

        moveDirection = Vector3.Scale(targetDirection, new Vector3(1, 0, 1)).normalized;
        moveDirection *= speed;

        //�ŏI�I�Ȉړ�����
        controller.Move(moveDirection * Time.deltaTime);
    }

    void RotateControl()
    {
        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        UpdateCursorLock();
    }

    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //�p�x�����֐��̍쐬
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
