using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] Camera cam;    //UI��\�����Ă���J�����I�u�W�F�N�g
    [SerializeField] Image enemy;   //enemy�̏ꏊ��\������UI
    private SkillManager skillManager;

    // Start is called before the first frame update
    void Start()
    {
        skillManager = GetComponent<SkillManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.rectTransform.anchoredPosition
               = WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position);
        enemy.enabled = skillManager.GetTargetMinimapFlag();
        if (skillManager.GetTargetMinimapFlag())
        {
            enemy.rectTransform.anchoredPosition
                = WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position);
        }
    }
    /// <summary>
    /// ���[���h���W���X�N���[�����W��(�A���J�[�����ɂ���̖Y��Ȃ��悤��)
    /// </summary>
    /// <param name="cam">�J�����I�u�W�F�N�g</param>
    /// <param name="worldPosition">���[���h���W</param>
    /// <returns>�X�N���[�����W</returns>
    private Vector3 WorldToScreenPoint(Camera cam, Vector3 worldPosition)
    {
        // �J������Ԃɕϊ�(�J�������猩�����W�ɕϊ�)
        Vector3 cameraSpace = cam.worldToCameraMatrix.MultiplyPoint(worldPosition);

        // �N���b�s���O��Ԃɕϊ�(cameraSpace�����͈̔͂ɍi���Ă�)
        Vector4 clipSpace = cam.projectionMatrix * new Vector4(cameraSpace.x, cameraSpace.y, cameraSpace.z, 1f);

        //�f�o�C�X���W�F�����[1�@�E��{1
        //�����Ă�̂͐��K��
        // �f�o�C�X���W�n�ɕϊ�
        Vector3 deviceSpace = new Vector3(clipSpace.x / clipSpace.w, clipSpace.y / clipSpace.w, clipSpace.z / clipSpace.w);

        // �X�N���[�����W�n�ɕϊ�
        Vector3 screenSpace = new Vector3((deviceSpace.x + 1f) * 0.21f * Screen.width, (deviceSpace.y + 1f) * 0.38f * Screen.height, deviceSpace.z);

        return screenSpace;
    }
}
