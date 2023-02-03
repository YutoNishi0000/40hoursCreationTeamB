using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewportController : Human
{
    [SerializeField] private float RECOGNIZE_DISTANCE = 10;       //プレイヤーを認識できる距離
    [SerializeField] private Text timerText;
    [SerializeField] private float time;

    private void Start()
    {
        timerText.enabled = false;
        time = 0;
    }

    void Update()
    {
        CheckPlayer();
    }

    void CheckPlayer()
    {
        //ターゲットからカメラの方向へ正規化したベクトルを作成
        Vector3 targetToCameraDirection_N = (transform.position - playerInstance.gameObject.transform.position).normalized;

        //正規化したベクトルの内積が一定以下なら見たことにする
        if (Vector3.Dot(targetToCameraDirection_N, transform.forward.normalized) < -0.5 && Vector3.Distance(transform.position, playerInstance.gameObject.transform.position) <= RECOGNIZE_DISTANCE)
        {
            print("見た！");

            //==========================================================================
            // ここで時間計測や判定を行う
            //==========================================================================
            CountTimer();
        }
        else
        {
            OffTimer();
        }
    }

    void CountTimer()
    {
        timerText.enabled = true;
        time += Time.deltaTime;
        timerText.text = Mathf.FloorToInt(time) + "秒";
    }

    void OffTimer()
    {
        time = 0;
        timerText.enabled = false;
    }
}
