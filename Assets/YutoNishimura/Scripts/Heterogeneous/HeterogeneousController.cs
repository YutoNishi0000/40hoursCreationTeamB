using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//異質なもの自身にアタッチするもの
[RequireComponent(typeof(MeshRenderer))]
public class HeterogeneousController : Actor
{
    private Material material;
    private readonly float destroyTime = 1.0f;
    private float dethtime;
    public bool takenPicFlag;       //写真を撮られたかどうか
    public bool enableTakePicFlag;  //サブカメラで写真を撮ることが可能かどうかを表すフラグ

    void Start()
    {
        enableTakePicFlag = false;
        takenPicFlag = false;
        material = GetComponent<Material>();
        dethtime = destroyTime;
    }

    void Update()
    {
        DestroyHeterogeneous();
    }

    //フラグを用いてこの関数を呼び出せばよい
    private void DestroyHeterogeneous()
    {
        if(!takenPicFlag)
        {
            return;
        }

        //アルファ値が０以下になったら自身を削除
        if (dethtime < 0)
        {
            //フィールドに足りない異質なものを補うときにクールタイム発生
            HeterogeneousSetter.CoolTime();
            //即座に自身のゲームオブジェクトを非アクティブに
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        dethtime -= Time.deltaTime;
    }

    public void SetEnableTakePicFlag(bool flag) { enableTakePicFlag = flag; }

    public bool GetEnableTakePicFlag() { return enableTakePicFlag; }

    public void SetTakenPicFlag(bool flag) { takenPicFlag = flag; }

    public bool GetTakenPicFlag() { return takenPicFlag; }
}