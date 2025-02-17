using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] Camera cam;    //UIを表示しているカメラオブジェクト
    [SerializeField] Image enemy;   //enemyの場所を表示するUI
    private SkillManager skillManager;

    // Start is called before the first frame update
    void Start()
    {
        skillManager = GetComponent<SkillManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RespawTarget.GetCurrentTargetObj() == null)
        {
            return;
        }
        enemy.rectTransform.anchoredPosition
               = WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position);
        enemy.enabled = skillManager.GetTargetMinimapFlag();
        if (skillManager.GetTargetMinimapFlag())
        {
            enemy.rectTransform.anchoredPosition
                = WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position);
        }
    }
    /// <summary>
    /// ワールド座標をスクリーン座標に(アンカー左下にするの忘れないように)
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
        Vector3 screenSpace = new Vector3((deviceSpace.x + 1f) * 0.21f * Screen.width, (deviceSpace.y + 1f) * 0.38f * Screen.height, deviceSpace.z);

        return screenSpace;
    }
}
