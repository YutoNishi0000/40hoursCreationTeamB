using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTime : MonoBehaviour
{
    // カメラのクールタイム
    [SerializeField] float coolTime = 3.0f;
    // カメラが操作できるか/できないか
    public bool IsEnable { get; private set; } = true;

    private IEnumerator StartTimer()
    {
        // カメラ使用不可
        IsEnable = false;
        // coolTime(3)秒待つ
        yield return new WaitForSeconds(coolTime);
        // coolTime(3)秒後カメラ使用可能
        IsEnable = true;
    }
    public void StartCoolTime()
    {
        if(!IsEnable)
        {
            return;
        }
        // コルーチンを開始する
        StartCoroutine(StartTimer());
    }
}
