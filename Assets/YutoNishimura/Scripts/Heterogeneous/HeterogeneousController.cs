using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//異質なもの自身にアタッチするもの
public class HeterogeneousController : MonoBehaviour
{
    private Material material;
    private readonly float destroyTime = 1;
    private float color_a;
    public bool takenPicFlag;       //写真を撮られたかどうか

    void Start()
    {
        takenPicFlag = false;
        material = GetComponent<Material>();
        color_a = destroyTime;
    }

    void Update()
    {
        DestroyHeterogeneous();
    }

    //消すとき、α値を減少させながら消滅させる
    //フラグを用いてこの関数を呼び出せばよい
    private void DestroyHeterogeneous()
    {
        if(!takenPicFlag)
        {
            return;
        }

        //アルファ値が０以下になったら自身を削除
        if (color_a < 0)
        {
            Destroy(gameObject);
        }

        color_a -= Time.deltaTime;

        GetComponent<Renderer>().material.color -= new Color(0, 0, 0, color_a);
    }
}