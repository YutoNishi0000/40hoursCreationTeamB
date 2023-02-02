using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //プレイヤーの移動速度
    float speed = 5.0f;

    //カメラオブジェクト
    public GameObject mainCamera;

    //z軸を調整
    public int zAddjust = 5;
    
    //x軸を調整
    public int xAddjust = 3;

    void Update()
    {
        //カメラはプレイヤーと同じ位置にする
        mainCamera.transform.position = new Vector3(transform.position.x + xAddjust, transform.position.y, transform.position.z - zAddjust);

        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += speed * transform.forward * Time.deltaTime;
        }

        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= speed * transform.forward * Time.deltaTime;
        }

        // Dキー（右移動）
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += speed * transform.right * Time.deltaTime;
        }

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= speed * transform.right * Time.deltaTime;
        }
    }
}
