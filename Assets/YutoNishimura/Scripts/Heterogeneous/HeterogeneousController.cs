using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//異質なもの自身にアタッチするもの
public class HeterogeneousController : Actor
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

    //自身がカメラに写っていた場合にだけ呼び出される
    void OnWillRenderObject()
    {
        //メインカメラから見えたときだけ処理を行う
        if (Camera.current.name == "Main Camera")
        {
            Vector3 strangeObjVec = transform.position - playerInstance.transform.position;
            Vector3 playerForwardVec = playerInstance.transform.forward;

            float angle = Vector3.Angle(playerForwardVec, strangeObjVec);

            //判定距離(後で上に移動させる)
            const float enableSeeDis = 7.0f;

            float judgeDis = strangeObjVec.magnitude * Mathf.Cos((angle / 360) * Mathf.PI * 2);

            if (judgeDis <= enableSeeDis && GameManager.Instance.IsSubPhoto)
            {
                //異質なものを撮った回数のカウントをインクリメント＋スコアに+10する
                GameManager.Instance.numSubShutter++;
                ScoreManger.Score += 10;

                if(GameManager.Instance.numSubShutter == 1)
                {
                    //タイムカウント開始
                    GameManager.Instance.skillManager.StartCount();
                }
                else if(GameManager.Instance.numSubShutter == 3)
                {
                    //スキル発動
                    GameManager.Instance.skillManager.SkillImposition();

                    GameManager.Instance.numSubShutter = 0;
                }

                //念のためここでもフラグはオフにしておく
                GameManager.Instance.IsSubPhoto = false;

                //自信を消滅させるフラグをオンに
                takenPicFlag = true;
            }

            //一応ここでフラグはオフにしておく
            GameManager.Instance.IsSubPhoto = false;
        }
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