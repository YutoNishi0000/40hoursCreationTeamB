using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �v���C���[�J�����Ǘ��N���X
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    //���C���J�����I�u�W�F�N�g
    private GameObject cameraObjct = null;
    // FPS���_(�J�����̎��_)
    [SerializeField] private Transform viewPoint;

    // �J�����̊��x
    private readonly float CAM_SENSITIVITY = 5.0f;

    // �J�����̏c���������̊p�x
    private readonly float CAM_V_ROT_CLAMP = 40f;

    // �}�E�X���͒l
    private Vector2 mouseInput;

    // �c�����̉�]�l
    private float verticalMouseInput;



    private void Awake()
    {
        //�J�����I�u�W�F�N�g�擾
        cameraObjct = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        ViewRotate();
    }
    private void LateUpdate()
    {
        //�J�����̃|�W�V�����𓮂����ėh�炷
        cameraObjct.transform.position = viewPoint.position;
        cameraObjct.transform.eulerAngles = viewPoint.transform.eulerAngles;
    }



    /// <summary>
    /// ���_�ύX�v�Z
    /// </summary>
    private void ViewRotate()
    {
        // �}�E�X�̓��͎擾
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X") * CAM_SENSITIVITY,
                                 Input.GetAxisRaw("Mouse Y") * CAM_SENSITIVITY);

        // �v���C���[��Y����]
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                              transform.eulerAngles.y + mouseInput.x,
                                              transform.eulerAngles.z);
        // �}�E�X�̏c�����𔽉f
        verticalMouseInput += mouseInput.y;

        // �c�����̃J�����ɐ���������
        verticalMouseInput = Mathf.Clamp(verticalMouseInput,
                                         -CAM_V_ROT_CLAMP,
                                         CAM_V_ROT_CLAMP);

        // ���_(viewPoint)�̏c�����ɑ��
        viewPoint.rotation = Quaternion.Euler(-verticalMouseInput,
                                              viewPoint.rotation.eulerAngles.y,
                                              viewPoint.rotation.eulerAngles.z);


    }
}
