using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5 : MonoBehaviour
{
    //ターゲットオブジェクト
    [SerializeField] GameObject target;
    //プレーヤーオブジェクト
    [SerializeField] GameObject player;
    //プレイヤーとターゲットの距離
    float distance = 0;
    //ターゲットの認識範囲
    const float area = 5;

    private TodayTask todayTask;

    //day5が終わりそうかどうか
    public static bool day5 = false;
    // Start is called before the first frame update
    void Start()
    {
        day5 = false;
        todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
    }

    // Update is called once per frame
    void Update()
    {
        //distance = Vector3.Distance(player.transform.position, target.transform.position);
        //if (distance < area)
        //{
        //    //タスクが一つでも残っていたら
        //    if (todayTask.todayTask.Count > 0)
        //    {
        //        Debug.Log("トゥルーエンド");
        //        GameManager.Instance.NextDay("SucsessNegotiation");
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        Debug.Log("バッドエンド");
        //        GameManager.Instance.NextDay("FailedNegotiation");
        //        Destroy(gameObject);
        //    }
        //}
    }
}
