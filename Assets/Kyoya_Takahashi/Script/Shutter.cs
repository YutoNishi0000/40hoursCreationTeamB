using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    //撮影のクールタイム
    [SerializeField] private float coolTime = 3f;
    //撮影可能か
    private bool isEnable = true;
    //撮影中か
    public static bool isFilming = false;
    void Update()
    {
        if(!isEnable)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("左クリックされた");
            SEManager.Instance.PlayShot();
            isFilming = true;
        }
        else
        {
            return;
        }
        // コルーチンを開始する
        StartCoroutine(StartTimer());
    }
    /// <summary>
    /// カメラのクールタイム
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartTimer()
    {
        // カメラ使用不可
        isEnable = false;
        // coolTime(3)秒待つ
        yield return new WaitForSeconds(coolTime);
        // coolTime(3)秒後カメラ使用可能
        isEnable = true;
    }
}
