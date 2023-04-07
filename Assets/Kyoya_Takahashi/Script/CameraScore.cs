using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Ώۂ���ʂ̒��S�ɂǂꂾ���߂��ɂ���ăX�R�A���o��
/// </summary>
public class CameraScore : MonoBehaviour
{
    //�r���[���W�ɕϊ��������I�u�W�F�N�g�|�W�V����
    [SerializeField] private GameObject obj = null;
    [SerializeField] private GameObject obj2 = null;

    [SerializeField] private Camera cam = null;

    //���ꂼ��̃X�R�A�̒l
    enum ScoreType
    {
        high  = 50,
        midle = 30,
        low   = 10,
        outOfScreen = 0,
    };
    public static int Score = 0;
    //���ꂼ��̔���̕�
    private float areaWidth = 192;      //(960 / 5) ���5����
    private float areaHeight = 216;     //(1080 / 5) ���5����
    //��ʂ̒��S
    private Vector3 center = Vector3.zero;

    private void Start()
    {
        //��ʂ̒��S�����߂�
        center = new Vector3(
            areaWidth * 2 + areaWidth * 0.5f,
            areaHeight * 2 + areaHeight * 0.5f,
            0.0f);
    }
    private void Update()
    {
        obj2.transform.position = WorldToScreenPoint(cam, DestroyTarget.target.transform.position);

        if (GameManager.Instance.IsPhoto)
        {
            Score = checkScore(WorldToScreenPoint(cam, DestroyTarget.target.transform.position));
            Debug.Log(Score);
            
        }

        //�Ȃ񂩃}�E�X�{�^���Ƃ�Ȃ�����Ԃ��

    }
    /// <summary>
    /// ���[���h���W���X�N���[�����W��
    /// </summary>
    /// <param name="cam">�J�����I�u�W�F�N�g</param>
    /// <param name="worldPosition">���[���h���W</param>
    /// <returns>�X�N���[�����W</returns>
    public static Vector3 WorldToScreenPoint(Camera cam, Vector3 worldPosition)
    {
        // �J������Ԃɕϊ�(�J�������猩�����W�ɕϊ�)
        Vector3 cameraSpace = cam.worldToCameraMatrix.MultiplyPoint(worldPosition);

        // �N���b�s���O��Ԃɕϊ�(cameraSpace�����͈̔͂ɍi���Ă�)
        Vector4 clipSpace = cam.projectionMatrix * new Vector4(cameraSpace.x, cameraSpace.y, cameraSpace.z, 1f);

        //�f�o�C�X���W�F�����[1�@�E��{1
        //�����Ă�̂͐��K
        // �f�o�C�X���W�n�ɕϊ�
        Vector3 deviceSpace = new Vector3(clipSpace.x / clipSpace.w, clipSpace.y / clipSpace.w, clipSpace.z / clipSpace.w);

        // �X�N���[�����W�n�ɕϊ�
        Vector3 screenSpace = new Vector3((deviceSpace.x + 1f) * 0.5f * Screen.width, (deviceSpace.y + 1f) * 0.5f * Screen.height, deviceSpace.z);

        return screenSpace;
    }
    /// <summary>
    /// �����ɂǂꂾ���߂�������
    /// </summary>
    /// <param name="scrPoint">�X�N���[�����W</param>
    /// <returns>�X�R�A�̒l</returns>
    private int checkScore(Vector3 scrPoint)
    {
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2)
        {
            return (int)ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth)
        {
            return (int)ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth * 2)
        {
            return (int)ScoreType.low;
        }
        return (int)ScoreType.outOfScreen;
    }
}