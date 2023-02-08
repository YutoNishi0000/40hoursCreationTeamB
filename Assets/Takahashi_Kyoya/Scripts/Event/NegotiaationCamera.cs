using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiaationCamera : MonoBehaviour
{
    //�ړ�����
    private const float moveDistance = 3;
    //�ړ��O�̃|�W�V����
    Vector3 latePosition = Vector3.zero;
    //�ړ���̃|�W�V����
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
