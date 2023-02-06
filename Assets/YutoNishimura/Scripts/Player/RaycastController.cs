using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public static float lockonTime = 0.0f;
    private float maxLockonTime;
    public static bool Lockon;
    public static bool BeatHeart;
    // Start is called before the first frame update
    void Start()
    {
        lockonTime = 0;
        Lockon = false;
        BeatHeart = false;
        maxLockonTime = 1;
    }

    private void FixedUpdate()
    {
        if (!Lockon)
        {
            RaycastHit hit;
            int layer = 1 << LayerMask.NameToLayer("Target");
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 100, layer))
            {
                //���C�Ƀq�b�g�����I�u�W�F�N�g���^�[�Q�b�g�Ȃ��
                //if (hit.collider.gameObject.CompareTag("Target"))
                {
                    BeatHeart = true;

                    Debug.Log("�n�[�g���ۓ����ł�");

                    //=============================================================================
                    //
                    if (GameManager.Instance.GetDate() != 1)
                    {
                        return;
                    }
                    //
                    //=============================================================================

                    lockonTime += Time.deltaTime;
                    if (lockonTime >= maxLockonTime)
                    {
                        Debug.Log("���b�N�I��");
                        Lockon = true;
                    }
                }
            }
            else
            {
                Debug.Log("���̂��̂ɓ������Ă܂�");
                lockonTime = 0.0f;
                BeatHeart = false;
            }
        }
    }
}
