using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerMove : MonoBehaviour
{
    public GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 3f, Ysensityvity = 3f;

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    public GameObject targetCenter;

    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (RayTest.lockon)
        {
            lockOnTargetObject(targetCenter);
        }
        else
        {
            float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
            float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

            cameraRot *= Quaternion.Euler(-yRot, 0, 0);
            characterRot *= Quaternion.Euler(0, xRot, 0);

            //Updateの中で作成した関数を呼ぶ
            cameraRot = ClampRotation(cameraRot);

            cam.transform.localRotation = cameraRot;
            transform.localRotation = characterRot;
        }
    }

    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

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
        transform.LookAt(target.transform, transform.up);
    }
}
