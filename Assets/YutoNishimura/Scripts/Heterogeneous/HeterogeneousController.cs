using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�َ��Ȃ��̎��g�ɃA�^�b�`�������
public class HeterogeneousController : MonoBehaviour
{
    private Material material;
    private readonly float destroyTime = 1;
    private float color_a;

    void Start()
    {
        material = GetComponent<Material>();
        color_a = destroyTime;
    }

    void Update()
    {

    }

    //�����Ƃ��A���l�����������Ȃ�����ł�����
    //�t���O��p���Ă��̊֐����Ăяo���΂悢
    private void DestroyHeterogeneous()
    {
        //�A���t�@�l���O�ȉ��ɂȂ����玩�g���폜
        if (color_a < 0)
        {
            Destroy(gameObject);
        }

        color_a -= Time.deltaTime;

        GetComponent<Renderer>().material.color -= new Color(0, 0, 0, color_a);
    }
}