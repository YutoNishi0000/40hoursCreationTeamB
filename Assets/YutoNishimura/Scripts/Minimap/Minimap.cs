using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーとターゲットの位置を把握
public class Minimap : Actor
{
    [SerializeField] private Vector2 fieldParam;         //フィールドの幅と高さ（スケールを10倍した値）
    [SerializeField] private Vector2 minimapParam;       //UIの大きさを設定 X:幅　Y:高さ（奥行）-> ピクセル値
    [SerializeField] private GameObject fieldObj;        //フィールドの親オブジェクト
    [SerializeField] private Image basePos;              //ミニマップのUIイメージ
    [SerializeField] private Image player;               //プレイヤーのUIイメージ
    [SerializeField] private Image target;               //対象のUIイメージ

    //メモ
    //プレイヤーと対象ののx座標、z座標をそれぞれ求めフィールドの幅、高さでそれぞれ割ると-1から1の範囲で表現することができるのでそれを利用する

    void Start()
    {

    }

    void Update()
    {
        //プレイヤーと対象の位置を常に更新して表示する
        //最終的にはピクセル計算になる
        player.rectTransform.position = GetMinimapPos(playerInstance.transform.position);
        target.rectTransform.position = GetMinimapPos(targetInstance.transform.position);
        Debug.Log(GetMinimapPos(playerInstance.transform.position));
    }

    //UI上でのプレイヤーと対象の位置を取得
    private Vector2 GetMinimapPos(Vector3 pos)
    {
        //-1から1の範囲に収縮し、UIように大きさを合わせる
        return (new Vector2(pos.x - fieldObj.transform.position.x, pos.z - fieldObj.transform.position.z) / (fieldParam / 2)) * (minimapParam / 2) + (Vector2)basePos.rectTransform.position;
    }
}
