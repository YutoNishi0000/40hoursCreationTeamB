using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤーカメラ管理クラス
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    //メインカメラオブジェクト
    private GameObject cameraObjct = null;
    // FPS視点(カメラの視点)
    [SerializeField] private Transform viewPoint;

    // カメラの感度
    private readonly float CAM_SENSITIVITY = 5.0f;

    // カメラの縦方向制限の角度
    private readonly float CAM_V_ROT_CLAMP = 40f;

    // マウス入力値
    private Vector2 mouseInput;

    // 縦方向の回転値
    private float verticalMouseInput;



    private void Awake()
    {
        //カメラオブジェクト取得
        cameraObjct = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        ViewRotate();
    }
    private void LateUpdate()
    {
        //カメラのポジションを動かして揺らす
        cameraObjct.transform.position = viewPoint.position;
        cameraObjct.transform.eulerAngles = viewPoint.transform.eulerAngles;
    }



    /// <summary>
    /// 視点変更計算
    /// </summary>
    private void ViewRotate()
    {
        // マウスの入力取得
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X") * CAM_SENSITIVITY,
                                 Input.GetAxisRaw("Mouse Y") * CAM_SENSITIVITY);

        // プレイヤーのY軸回転
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                              transform.eulerAngles.y + mouseInput.x,
                                              transform.eulerAngles.z);
        // マウスの縦方向を反映
        verticalMouseInput += mouseInput.y;

        // 縦方向のカメラに制限をつける
        verticalMouseInput = Mathf.Clamp(verticalMouseInput,
                                         -CAM_V_ROT_CLAMP,
                                         CAM_V_ROT_CLAMP);

        // 視点(viewPoint)の縦方向に代入
        viewPoint.rotation = Quaternion.Euler(-verticalMouseInput,
                                              viewPoint.rotation.eulerAngles.y,
                                              viewPoint.rotation.eulerAngles.z);


    }
}
