using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Human
{
    private Camera mainCam;

    Vector3 targetDirection;        //�ړ���������̃x�N�g��
    Vector3 moveDirection = Vector3.zero;

    private CharacterController controller;

    public float speed;         //�L�����N�^�[�̈ړ����x
    public float jumpSpeed;     //�L�����N�^�[�̃W�����v��
    public float rotateSpeed;   //�L�����N�^�[�̕����]�����x
    public float gravity;       //�L�����ɂ�����d�͂̑傫��

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        rotateSpeed = 10f;
        gravity = 20f;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
    }

    #region �ړ��֘A����

    public override void MoveControl()
    {
        //���i�s�����v�Z
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

        //���s�A�j���[�V�����Ǘ�
        AnimationControl(v, h);

        //��]����
        RotationControl();

        //�ŏI�I�Ȉړ�����
        controller.Move(moveDirection * Time.deltaTime);
    }

    //�I�[�o�[���C�h�֐�
    public void AnimationControl(float vertical, float height)
    {
        //���s�A�j���[�V�����Ǘ�
        if (vertical > .1 || vertical < -.1 || height > .1 || height < -.1) //(�ړ����͂������)
        {
            animator.SetFloat("Speed", 1f); //�L�������s�̃A�j���[�V�����J�n
        }
        else    //(�ړ����͂�������)
        {
            animator.SetFloat("Speed", 0); //�L�������s�̃A�j���[�V�����I��
        }
    }

    //��]����
    void RotationControl()  //�L�����N�^�[���ړ�������ς���Ƃ��̏���
    {
        Vector3 rotateDirection = moveDirection;
        rotateDirection.y = 0;

        //����Ȃ�Ɉړ��������ω�����ꍇ�݈̂ړ�������ς���
        if (rotateDirection.sqrMagnitude > 0.01)
        {
            //�ɂ₩�Ɉړ�������ς���
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.Slerp(transform.forward, rotateDirection, step);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    #endregion
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
}
