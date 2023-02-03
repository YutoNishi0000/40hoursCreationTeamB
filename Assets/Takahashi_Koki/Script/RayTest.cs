using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    public static float lockonTime = 0.0f;
    public float maxLockonTime = 3.0f;
    public static bool lockon;
    // Start is called before the first frame update
    void Start()
    {
        lockonTime = 0;
        lockon = false;
    }

    private void FixedUpdate()
    {
        if (!lockon)
        {
            RaycastHit hit;
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 100))
            {
                //���C�Ƀq�b�g�����I�u�W�F�N�g���^�[�Q�b�g�Ȃ��
                if (hit.collider.gameObject.CompareTag("Target"))
                {
                    lockonTime += Time.deltaTime;
                    if (lockonTime >= maxLockonTime)
                    {
                        lockon = true;
                    }
                }
            }
            else
            {
                lockonTime = 0.0f;
            }
        }

    }
}
