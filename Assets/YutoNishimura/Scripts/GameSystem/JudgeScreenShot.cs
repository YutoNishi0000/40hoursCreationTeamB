using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class JudgeScreenShot : MonoBehaviour
{
    public JudgeTarget judgeTarget;
    public JudgeSubTarget judgeSubTarget;

    //コンストラクタ
    public JudgeScreenShot()
    {
        judgeTarget = new JudgeTarget();
        judgeSubTarget = new JudgeSubTarget();
    }
}

//ターゲット判定だけを行うクラス
public class JudgeTarget : MonoBehaviour
{
    //それぞれのスコアの値
    public enum ScoreType
    {
        high = 50,
        midle = 30,
        low = 10,
        outOfScreen = 0,
    };

    /// <summary>
    /// ターゲットを撮影できているか
    /// </summary>
    /// <param name="player">プレイヤーオブジェクト</param>
    /// <param name="targetModel">撮影時に表示したいオブジェクト</param>
    /// <param name="cam">プレイヤーカメラオブジェクト</param>
    /// <param name="worldPosition">ターゲットのワールド座標</param>
    /// <param name="center">プレイヤー画面カメラの中心</param>
    /// <param name="areaWidth">プレイヤー画面の幅</param>
    /// <param name="areaHeight">プレイヤー画面の高さ</param>
    /// <param name="raise">スコア倍率</param>
    /// <returns>撮影成功：true、撮影失敗：false</returns>
    public bool ShutterTarget(
        GameObject player,
        GameObject targetModel,
        Camera cam,
        Vector3 worldPosition,
        Vector3 center,
        float areaWidth,
        float areaHeight,
        float raise)
    {
        //SE再生
        SEManager.Instance.PlayShot();
        //プレイヤーとターゲットの間にオブジェクトがあるかどうか
        //===== 間にオブジェクトがあるとき =====
        if (createRay(player))
        {
            return false;
        }
        //===== 間にオブジェクトがないとき =====
        //ターゲットが中心にどれだけ近いかによってスコアを決める
        ScoreType finalScore 
            = checkScore(WorldToScreenPoint(cam, worldPosition), center, areaWidth, areaHeight);

        switch (finalScore)
        {
            case ScoreType.low:
                TargetProcess(targetModel, ScoreType.low, raise);
                return true;
            case ScoreType.midle:
                TargetProcess(targetModel, ScoreType.midle, raise);
                return true;
            case ScoreType.high:
                TargetProcess(targetModel, ScoreType.high, raise);
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// 対象を撮影したときの最低限の処理
    /// </summary>
    /// <param name="targetModel">撮影時に写真に写したいオブジェクト</param>
    /// <param name="type">enum型のそれぞれのスコア加算量</param>
    /// <param name="raise">スコア加算倍率</param>
    private void TargetProcess(GameObject targetModel, ScoreType type, float raise)
    {
        GameObject targetObj = RespawTarget.GetCurrentTargetObj();
        //手動でターゲットが表示される位置を調整
        Vector3 targetPos = new Vector3(targetObj.transform.position.x, targetObj.transform.position.y - 1, targetObj.transform.position.z);
        Instantiate(targetModel, targetPos, targetObj.transform.rotation);
        //スコア加算
        ScoreManger.Score += GetFinalScore(type, SkillManager.GetAddScoreFlag(), raise);
        //時間を獲得
        CountDownTimer.IncreaceTime();
        //対象を撮影した回数をインクリメント
        GameManager.Instance.numTargetShutter++;
        //SE
        SEManager.Instance.PlayTargetShot();
        SEManager.Instance.PlayPlusTimeCountSE();
        //ターゲットリスポーン
        RespawTarget.RespawnTarget();
    }

    /// <summary>
    /// スコア倍率も含めた最終的なスコア
    /// </summary>
    /// <param name="type">enum型のそれぞれのスコア加算量</param>
    /// <param name="flag">スコアアップフラグ</param>
    /// <param name="raise">スコアアップ倍率</param>
    /// <returns></returns>
    public float GetFinalScore(ScoreType type, bool flag, float raise)
    {
        switch (type)
        {
            case ScoreType.low:
                GameManager.Instance.numLowScore++;
                return flag ? (float)ScoreType.low * raise : (float)ScoreType.low;
            case ScoreType.midle:
                GameManager.Instance.numMiddleScore++;
                return flag ? (float)ScoreType.midle * raise : (float)ScoreType.midle;
            case ScoreType.high:
                GameManager.Instance.numHighScore++;
                return flag ? (float)ScoreType.high * raise : (float)ScoreType.high;
            default:
                return 0;
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
    private ScoreType checkScore(Vector3 scrPoint, Vector3 center, float areaWidth, float areaHeight)
    {
        //return lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));
        ScoreType defaultScore = lower(checkScoreHori(scrPoint, center, areaWidth), checkScoreVart(scrPoint, center, areaHeight));

        return defaultScore;
    }
    /// <summary>
    /// スコアの判定(横)
    /// </summary>
    /// <param name="scrPoint">スクリーン座標</param>
    /// <param name="score">タテだけで見たときのスコア</param>
    /// <returns>スコア</returns>
    private ScoreType checkScoreHori(Vector3 scrPoint, Vector3 center, float areaWidth)
    {
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2)
        {
            return ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth)
        {
            return ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth * 2)
        {
            return ScoreType.low;
        }
        return ScoreType.outOfScreen;
    }
    /// <summary>
    /// スコアの判定(縦)
    /// </summary>
    /// <param name="scrPoint">スクリーン座標</param>
    /// <param name="score">タテだけで見たときのスコア</param>
    /// <returns>スコア</returns>
    private ScoreType checkScoreVart(Vector3 scrPoint, Vector3 center, float areaHeight)
    {
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2)
        {
            return ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2 + areaHeight)
        {
            return ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2 + areaHeight * 2)
        {
            return ScoreType.low;
        }
        return ScoreType.outOfScreen;
    }
    /// <summary>
    /// 低いほうの値をを求める
    /// </summary>
    /// <param name="v">片方のスコア</param>
    /// <returns>スコア</returns>
    private ScoreType lower(ScoreType v1, ScoreType v2)
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
    public bool createRay(GameObject player)
    {
        Vector3 diff = RespawTarget.GetCurrentTargetObj().transform.position - player.gameObject.transform.position;

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
}

//異質なものの判定だけを行うクラス
public class JudgeSubTarget : MonoBehaviour
{
    //これ使う
    public bool ShutterSubTargets(Camera camera, GameObject playerPos, List<GameObject> list, float judgeDis)
    {
        float fov = camera.fieldOfView;
        float judgeRange = Mathf.Cos(Mathf.PI - (((2 * Mathf.PI) - ((fov / 360) * Mathf.PI * 2)) / 2));
        Vector3 playerForwardVec = camera.transform.forward;   //念のためカメラが向いている方向のベクトルを取得
        int tempNumSubTargets = GameManager.Instance.numTargetShutter;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
            {
                Vector3 playerToSubVec = list[i].transform.position - playerPos.transform.position;
                float dot = Vector3.Dot(playerToSubVec.normalized, playerForwardVec.normalized);

                if (playerToSubVec.magnitude < judgeDis && dot >= judgeRange)
                {
                    //サブカメラカウントをインクリメント
                    GameManager.Instance.numSubShutter++;
                    //スコアを加算
                    ScoreManger.Score += 10;
                    //tempList[i]のオブジェクトの消滅フラグをオンにす 
                    list[i].GetComponent<HeterogeneousController>().SetTakenPicFlag(true);
                }
            }
        }

        //上の処理を通して、撮影した異質なものの個数が増えていたら
        if ((GameManager.Instance.numSubShutter - tempNumSubTargets) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}