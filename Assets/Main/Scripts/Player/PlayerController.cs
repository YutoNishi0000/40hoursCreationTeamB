using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Human
{
    Vector3 moveDirection = Vector3.zero;
    Vector3 targetDirection;        //�ړ���������̃x�N�g��
    private CharacterController controller;
    [SerializeField] private float speed = 5f;
    public PlayerStateController playerState;

    public GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 3f, Ysensityvity = 3f;

    bool cursorLock = true;
    public bool _moveLock;                           //�s���𐧌����邩�ǂ���

    //public GameObject targetCenter;
    private float jumpSpeed = 5f;
    private float gravity = 20f;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX = -40f, maxX = 40f;
    Vector3 initialCamPos;

    private void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        initialCamPos = cam.transform.localPosition;
        controller = GetComponent<CharacterController>();
        playerState = GetComponent<PlayerStateController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _moveLock = true;
        Message.PlayerMoveFlag = true;
        //Debug.Log(Message.PlayerMoveFlag);
        if (_moveLock || !Message.PlayerMoveFlag)
        {
            return;
        }

        switch (playerState.GetPlayerState())
        {
            case PlayerStateController.PlayerState.ViewportLocked:
                //lockOnTargetObject(targetCenter);
                break;
            default:
                RotateControl();
                break;
        }
    }

    private void FixedUpdate()
    {
        MoveControl();
    }

    public override void MoveControl()
    {
        if(_moveLock || !Message.PlayerMoveFlag)
        {
            return;
        }

        //�L�[�{�[�h���͂��擾
        float v = Input.GetAxisRaw("Vertical");         //InputManager�́����̓���       
        float h = Input.GetAxisRaw("Horizontal");       //InputManager�́����̓��� 

        //�J�����̐��ʕ����x�N�g������Y�����������A���K�����ăL����������������擾
        Vector3 forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Camera.main.transform.right; //�J�����̉E�������擾

        //�J�����̕������l�������L�����̐i�s�������v�Z
        targetDirection = h * right + v * forward;

        AnimatioControl(v, h);

        //�n��ɂ���ꍇ�̏���
        if (controller.isGrounded)
        {
            Debug.Log("�n�ʂɂ��Ă��܂�");
            //�ړ��̃x�N�g�����v�Z
            moveDirection = targetDirection * speed;

            //Jump�{�^���ŃW�����v����
            if (Input.GetButton("Jump"))
            {
                Debug.Log("�W�����v");
                moveDirection.y = jumpSpeed;
            }
        }
        else        //�󒆑���̏����i�d�͉����x���j
        {
            float tempy = moveDirection.y;
            //(���̂Q���̏���������Ƌ󒆂ł����͕����ɓ�����悤�ɂȂ�)
            //moveDirection = Vector3.Scale(targetDirection, new Vector3(1, 0, 1)).normalized;
            //moveDirection *= speed;
            moveDirection.y = tempy - gravity * Time.deltaTime;
        }

        //�ŏI�I�Ȉړ�����
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void AnimatioControl(float vertical, float horizon)
    {
        if (vertical > .1 || vertical < -.1 || horizon > .1 || horizon < -.1) //(�ړ����͂������)
        {
            cam.transform.localPosition = new Vector3(initialCamPos.x, initialCamPos.y + Mathf.PingPong(Time.time / 2, 0.2f), initialCamPos.z);
        }
        else
        {
            cam.transform.localPosition = initialCamPos;
        }
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

        //UpdateCursorLock();
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

    private void lockOnTargetObject(GameObject target)
    {
        Debug.Log("��ʃ��b�N���ł�");
        transform.LookAt(target.transform.position);
    }
}



//�l�^�N���X�̊��N���X
public class Human : MonoBehaviour
{
    //�v���C���[�̃C���X�^���X
    protected PlayerController playerInstance;

    private void Awake()
    {
        //�Q�[���J�n���v���C���[�C���X�^���X���擾
        playerInstance = FindObjectOfType<PlayerController>();
    }

    public virtual void MoveControl() { }
    public virtual void AnimationControl() { }

    /// <summary>
    /// �w�肵���Q�[���I�u�W�F�N�g���w�肵�������Ɍ�������
    /// </summary>
    /// <param name="gameObject">�@�w�肵�������Ɍ����������Q�[���I�u�W�F�N�g </param>
    /// <param name="direction"> �Q�[���I�u�W�F�N�g���������������� </param>
    public void CorrectRotation(GameObject gameObject, Vector3 direction)
    {
        gameObject.transform.DORotate(direction, 0.5f);
    }
}
