using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiaationCamera : MonoBehaviour
{
    //移動距離
    private const float moveDistance = 3;
    //移動前のポジション
    Vector3 latePosition = Vector3.zero;
    //移動先のポジション
    Vector3 position = Vector3.zero;
    private void Awake()
    {
        latePosition = transform.position;
        position = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        position.z = Mathf.Lerp(transform.position.z, latePosition.z + moveDistance, 0.02f);
        transform.position = position;
    }
}
