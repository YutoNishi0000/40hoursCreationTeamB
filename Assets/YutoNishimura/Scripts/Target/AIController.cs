using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//AI�ɕK�v�Ȋ֐��Ȃǂ��`�������N���X
public class AIController : Actor
{
    protected NavMeshAgent agent;    //�K���p�����GetComponent���邱��
    protected int destPoint;         //�K���p����ŏ��������邱��

    public virtual void GoNextPoint(List<GameObject> points)
    {
        //�n�_�������ݒ肳��Ă��Ȃ��ꍇ
        if (points.Count == 0)
        {
            return;
        }

        //�G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ�
        agent.destination = new Vector3(points[destPoint].transform.position.x, transform.position.y, points[destPoint].transform.position.z);

        //�z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A�K�v�Ȃ�Ώo���n�_�ɖ߂�
        destPoint = (destPoint + 1) % points.Count;
    }

    /// <summary>
    /// �|�C���g���Z�b�g����֐�
    /// </summary>
    /// <param name="prent"></param>
    /// <param name="points"></param>
    /// <param name="startNum">���Ԗڂ̃|�C���g����X�^�[�g���邩</param>
    public virtual void SetPoints(GameObject prent, List<GameObject> points, int startNum)
    {
        //points = null;
        // �q�I�u�W�F�N�g�̐����擾
        int childCount = prent.transform.childCount;

        // �q�I�u�W�F�N�g�����Ɏ擾����
        for (int i = startNum; i < childCount; i++)
        {
            i = i % childCount;
            Transform childTransform = prent.transform.GetChild(i);
            points.Add(childTransform.gameObject);
        }
    }

    public virtual void SetRootType() { }
}
