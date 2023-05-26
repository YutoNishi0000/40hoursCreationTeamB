using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] Camera cam;    //UIを表示しているカメラオブジェクト
    [SerializeField] Image enemy;   //enemyの場所を表示するUI
    private SkillManager skillManager;

    private const float ORTHO_SIZE = 70;
    // Start is called before the first frame update
    void Start()
    {
        skillManager = GetComponent<SkillManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.rectTransform.anchoredPosition
               = WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position);
        //enemy.enabled = skillManager.GetTargetMinimapFlag();
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
        // 平行投影のProjectionMatrixを計算する
        var aspectRatio = (float)Screen.width / Screen.height;
        var orthoWidth = ORTHO_SIZE * aspectRatio;
        var projMatrix = Matrix4x4.Ortho(orthoWidth * -1, orthoWidth, ORTHO_SIZE * -1, ORTHO_SIZE, 0, 1000);
        // カメラ空間に変換(カメラから見た座標に変換)
        Vector3 cameraSpace = cam.worldToCameraMatrix.MultiplyPoint(worldPosition);

        // クリッピング空間に変換(cameraSpaceを一定の範囲に絞ってる)
        //Vector4 clipSpace = cam.projectionMatrix * new Vector4(cameraSpace.x, cameraSpace.y, cameraSpace.z, 1f);
        Vector4 clipSpace = projMatrix * new Vector4(cameraSpace.x, cameraSpace.y, cameraSpace.z, 1f);

        //デバイス座標：左下ー1　右上＋1
        //割ってるのは正規化
        // デバイス座標系に変換
        Vector3 deviceSpace = new Vector3(clipSpace.x / clipSpace.w, clipSpace.y / clipSpace.w, clipSpace.z / clipSpace.w);

        // スクリーン座標系に変換
        Vector3 screenSpace = new Vector3((deviceSpace.x + 1f) * 0.25f * Screen.width, (deviceSpace.y + 1f) * 0.5f * Screen.height, deviceSpace.z);

        return screenSpace;
    }
}
