using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    public static GameObject target = null;
    //�^�[�Q�b�g�I�u�W�F�N�g�̃v���n�u
    [SerializeField] GameObject respawnTarget = null;
    private void Awake()
    {
        Respawntarget();
    }
    private void LateUpdate()
    {
        if (!GameManager.Instance.IsPhoto)
        {
            return;
        }
        if(ScoreManger.Score == 0)
        {
            return;
        }
        Debug.Log("�ʂ��Ă�");
        StartCoroutine(destroy());
        GameManager.Instance.IsPhoto = false;
    }
    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(target);
        yield return new WaitForSeconds(1);
        //Debug.Log("�ʂ��Ă�");
        Respawntarget();
    }
    private void Respawntarget()
    {
        //�^�[�Q�b�g�����݂��Ă���^�[�Q�b�g����
        target = Instantiate(respawnTarget,
            this.transform.position,
            Quaternion.Euler(0.0f, 0.0f, 0.0f));
        
    }
}
