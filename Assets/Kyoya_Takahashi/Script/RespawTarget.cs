using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawTarget : MonoBehaviour
{
    //�^�[�Q�b�g�����݂��Ă��邩
    public static bool isExistsTarget = true;
    //�^�[�Q�b�g�I�u�W�F�N�g
    [SerializeField] GameObject target = null;

    private void LateUpdate()
    {
        if(isExistsTarget)
        {
            return;
        }
        //�^�[�Q�b�g�����݂��Ă���^�[�Q�b�g����
        Instantiate(target,
            this.transform.position,
            Quaternion.Euler(0.0f, 0.0f, 0.0f));
        isExistsTarget = true;
    }
}
