using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeScore : ScoreManger
{
    //ビュー座標に変換したいオブジェクトポジション
    [SerializeField] private GameObject obj = null;

    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject player = null;
    //それぞれのスコアの値
    public enum ScoreType
    {
        high = 50,
        midle = 30,
        low = 10,
        outOfScreen = 0,
    };

    //スコア倍率(レベルに応じて値が異なる)
    private readonly float Odds_Level1 = 1.2f;
    private readonly float Odds_Level2 = 1.5f;
    private readonly float Odds_Level3 = 2.0f;

    //それぞれの判定の幅
    private const float areaWidth = 192;      //(960 / 5) 画面5等分
    private const float areaHeight = 216;     //(1080 / 5) 画面5等分

    //画面の中心
    static Vector3 center = Vector3.zero;

    //カメラのクールタイム
    private float coolTime = 3;
    //カメラ使用可能か
    private bool cameraEnable = true;
    private const float raiseScore = 1.5f;     //スコア上昇倍率
    private ScreenShot screen;
    [SerializeField] private Image getTimeImg;

    private void Start()
    {
        getTimeImg.enabled = false;
        //画面の中心を求める
        center = new Vector3(
            areaWidth * 2 + areaWidth * 0.5f,
            areaHeight * 2 + areaHeight * 0.5f,
            0.0f);

        screen = GameObject.FindObjectOfType<ScreenShot>();
    }
    private void LateUpdate()
    {
        if (Shutter.isFilming)
        {
            Shutter.isFilming = false;
            //Debug.Log("通ってる(1)");
            //障害物があるとき
            if (createRay())
            {
                Debug.Log("障害蟻");
                SEManager.Instance.PlayShot();
                return;
            }
            //ターゲットが画面外か
            if (checkScore(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position)) == (int)ScoreType.outOfScreen)
            {
                SEManager.Instance.PlayShot();
                Debug.Log("画面外");
                return;
            }

            //時間を獲得
            CountDownTimer.IncreaceTime();
            Debug.Log("時間を獲得しました");
            //ターゲットが撮影された
            screen.SetPhotographTargetFlag(false);
            //障害物がないときの処理
            Debug.Log("撮影した");
            //スコア加算
            ScoreManger.Score += checkScore(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position));
            ScoreManger.ShotMainTarget = true;
            TargetManager.IsSpawn = true;
            //対象を撮影した回数をインクリメント
            GameManager.Instance.numTargetShutter++;
            //SE
            SEManager.Instance.PlayTargetShot();
            //ターゲットリスポーン
            RespawTarget.RespawnTarget();
            //アニメーション開始
            Invoke("startTA", 0.2f);

            //screen.FadeIn(getTimeImg);
        }
    }
    /// <summary>
    /// ワールド座標をスクリーン座標に
    /// </summary>
    /// <param name="cam">カメラオブジェクト</param>
    /// <param name="worldPosition">ワールド座標</param>
    /// <returns>スクリーン座標</returns>
    private Vector3 WorldToScreenPoint(Camera cam, Vector3 worldPosition)
    {
        // カメラ空間に変換(カメラから見た座標に変換)
        Vector3 cameraSpace = cam.worldToCameraMatrix.MultiplyPoint(worldPosition);

        // クリッピング空間に変換(cameraSpaceを一定の範囲に絞ってる)
        Vector4 clipSpace = cam.projectionMatrix * new Vector4(cameraSpace.x, cameraSpace.y, cameraSpace.z, 1f);

        //デバイス座標：左下ー1　右上＋1
        //割ってるのは正規化
        // デバイス座標系に変換
        Vector3 deviceSpace = new Vector3(clipSpace.x / clipSpace.w, clipSpace.y / clipSpace.w, clipSpace.z / clipSpace.w);

        // スクリーン座標系に変換
        Vector3 screenSpace = new Vector3((deviceSpace.x + 1f) * 0.25f * Screen.width, (deviceSpace.y + 1f) * 0.5f * Screen.height, deviceSpace.z);

        return screenSpace;
    }
    /// <summary>
    /// 中央にどれだけ近いか判定
    /// </summary>
    /// <param name="scrPoint">スクリーン座標</param>
    /// <returns>スコアの値</returns>
    private float checkScore(Vector3 scrPoint)
    {
        //return lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));
        int defaultScore = lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));

        //スキルによるスコア加算フラグがオンだったらif内の処理を実行
        if (GameManager.Instance.skillManager.GetAddScoreFlag())
        {
            //フラグがオンだったら1.5倍のスコアを返す
            return defaultScore * raiseScore;
        }
        else
        {
            return defaultScore;
        }
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
        if (v1 < v2)
        {
            return v1;
        }
        else
        {
            return v2;
        }
    }
    RaycastHit hit;
    private bool createRay()
    {
        Vector3 diff = RespawTarget.GetCurrentTargetObj().transform.position - player.transform.position;

        Vector3 direction = diff.normalized;
        //Debug.Log(direction);
        float distance = Vector3.Distance(RespawTarget.GetCurrentTargetObj().transform.position, player.transform.position);
        Debug.DrawRay(player.transform.position, distance * direction, Color.green, 1f);
        if (Physics.Raycast(player.transform.position, direction, out hit, distance))
        {
            //Physics.Raycast(player.transform.position, direction, out hit, distance);
           // spere.transform.position = hit.point;
            Debug.Log("ステージに当たってる");
            return true;
        }
        Debug.Log("ステージに当たってない");
        return false;     
        
    }
    void startTA()
    {
        ShutterAnimation.TargetAnimationStart();
    }
}

