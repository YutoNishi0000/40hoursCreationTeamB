using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeScore : ScoreManger
{
    //ビュー座標に変換したいオブジェクトポジション
    [SerializeField] private GameObject obj = null;
    //[SerializeField] private GameObject obj2 = null;

    [SerializeField] private Camera cam = null;

    //それぞれのスコアの値
    enum ScoreType
    {
        high = 50,
        midle = 30,
        low = 10,
        outOfScreen = 0,
    };
    //それぞれの判定の幅
    private float areaWidth = 192;      //(960 / 5) 画面5等分
    private float areaHeight = 216;     //(1080 / 5) 画面5等分
    //画面の中心
    private Vector3 center = Vector3.zero;

    private void Start()
    {
        //画面の中心を求める
        center = new Vector3(
            areaWidth * 2 + areaWidth * 0.5f,
            areaHeight * 2 + areaHeight * 0.5f,
            0.0f);
    }
    private void Update()
    {
        //obj2.transform.position = WorldToScreenPoint(cam, DestroyTarget.target.transform.position);
        Debug.Log(ScoreManger.Score);
        if (GameManager.Instance.IsPhoto)
        {
            Debug.Log("通ってる");
            ScoreManger.Score += checkScore(WorldToScreenPoint(cam, DestroyTarget.target.transform.position));
            GameManager.Instance.IsPhoto = false;
        }
    }
    /// <summary>
    /// ワールド座標をスクリーン座標に
    /// </summary>
    /// <param name="cam">カメラオブジェクト</param>
    /// <param name="worldPosition">ワールド座標</param>
    /// <returns>スクリーン座標</returns>
    public static Vector3 WorldToScreenPoint(Camera cam, Vector3 worldPosition)
    {
        // カメラ空間に変換(カメラから見た座標に変換)
        Vector3 cameraSpace = cam.worldToCameraMatrix.MultiplyPoint(worldPosition);

        // クリッピング空間に変換(cameraSpaceを一定の範囲に絞ってる)
        Vector4 clipSpace = cam.projectionMatrix * new Vector4(cameraSpace.x, cameraSpace.y, cameraSpace.z, 1f);

        //デバイス座標：左下ー1　右上＋1
        //割ってるのは正規
        // デバイス座標系に変換
        Vector3 deviceSpace = new Vector3(clipSpace.x / clipSpace.w, clipSpace.y / clipSpace.w, clipSpace.z / clipSpace.w);

        // スクリーン座標系に変換
        Vector3 screenSpace = new Vector3((deviceSpace.x + 1f) * 0.5f * Screen.width, (deviceSpace.y + 1f) * 0.5f * Screen.height, deviceSpace.z);

        return screenSpace;
    }
    /// <summary>
    /// 中央にどれだけ近いか判定
    /// </summary>
    /// <param name="scrPoint">スクリーン座標</param>
    /// <returns>スコアの値</returns>
    private int checkScore(Vector3 scrPoint)
    {
        return lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));
    }
    /// <summary>
    /// スコアの判定(横)
    /// </summary>
    /// <param name="scrPoint">スクリーン座標</param>
    /// <param name="score">タテだけで見たときのスコア</param>
    /// <returns>スコア</returns>
    private int checkScoreHori(Vector3 scrPoint)
    {
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2)
        {
            return (int)ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth)
        {
            return (int)ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth * 2)
        {
            return (int)ScoreType.low;
        }
        return (int)ScoreType.outOfScreen;
    }
    /// <summary>
    /// スコアの判定(縦)
    /// </summary>
    /// <param name="scrPoint">スクリーン座標</param>
    /// <param name="score">タテだけで見たときのスコア</param>
    /// <returns>スコア</returns>
    private int checkScoreVart(Vector3 scrPoint)
    {
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2)
        {
            return (int)ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2 + areaHeight)
        {
            return (int)ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2 + areaHeight * 2)
        {
            return (int)ScoreType.low;
        }
        return (int)ScoreType.outOfScreen;
    }
    /// <summary>
    /// 低いほうの値をを求める
    /// </summary>
    /// <param name="v">片方のスコア</param>
    /// <returns>スコア</returns>
    private int lower(int v1, int v2)
    {
        if(v1 < v2)
        {
            return v1;
        }
        else
        {
            return v2;
        }
    }

}
