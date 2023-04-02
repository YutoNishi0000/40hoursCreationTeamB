using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーとターゲットの位置を把握
public class Minimap : Actor
{
    [SerializeField] private Vector2 fieldParam;         //フィールドの幅と高さ
    [SerializeField] private Vector2 minimapParam;       //UI上で表示するUIの大きさを指定
    [SerializeField] private GameObject fieldObj;        //フィールドの親オブジェクトを取得
    [SerializeField] private Image basePos;              //ミニマップを表示する基準点（ミニマップのテクスチャを挿入）
    [SerializeField] private Image player;               //プレイヤーのテクスチャ
    [SerializeField] private Image target;               //対象のテクスチャ

    //メモ
    //プレイヤーと対象ののx座標、z座標をそれぞれ求めフィールドの幅、高さでそれぞれ割ると-1から1の範囲で表現することができるのでそれを利用する

    void Start()
    {

    }

    void Update()
    {
        //プレイヤーと対象の位置を常に更新して表示する
        player.rectTransform.position = GetMinimapPos(playerInstance.transform.position);
        target.rectTransform.position = GetMinimapPos(targetInstance.transform.position);
    }

    //UI上でのプレイヤーと対象の位置を取得
    private Vector2 GetMinimapPos(Vector3 pos)
    {
        //-1から1の範囲に収縮し、UIように大きさを合わせる
        return (new Vector2(pos.x - fieldObj.transform.position.x, pos.z - fieldObj.transform.position.z) / fieldParam) * minimapParam + (Vector2)basePos.rectTransform.position;
    }
}
