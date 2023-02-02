using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMouseInputController : MonoBehaviour
{
    private Vector3 _currentmousePos;      //現在のマウス位置
    private Vector3 _previousemousePos;    //1フレーム前のマウスの位置
    private float _decisionRight;          //判定レーンの右側のx値
    private float _decisionLeft;           //判定レーンの左側のx値
    private readonly float LANE_WIDTH = 200;
    private bool _moiseAction;

    // Start is called before the first frame update
    void Start()
    {
        //スクリーンの幅を格納
        _decisionRight = Screen.width;
        _decisionLeft = Screen.width - LANE_WIDTH;
    }

    private void FixedUpdate()
    {
        //現在のマウスの位置を取得
        _currentmousePos = Input.mousePosition;

        //ここで何かしらの処理を加える
        //もしも1フレーム前のマウスの位置と現在のマウスの位置が違っていて、現在のマウスの位置がレーン内にあるのであれば
        if(_currentmousePos != _previousemousePos && _decisionLeft <= _currentmousePos.x && _currentmousePos.x <= _decisionRight)
        {
            Debug.Log("マウスの位置が変わった");
            _moiseAction = true;

            //==============================================================
            // ここで判定を取る
            //==============================================================
        }
        else
        {
            _moiseAction = false;
        }

        _previousemousePos = _currentmousePos;
    }

    public bool GetMouseAction()
    {
        return _moiseAction;
    }
}
