using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    //public static float lockonTime = 0.0f;
    //private float maxLockonTime;
    //public static bool Lockon;

    ////ターゲット
    //GameObject target = null;
    ////
    //GameObject todayTaskUI = null;
    //TodayTask todayTask = null; 

    ////拍動アニメーション
    //HeartBeat heartBeat = null;

    //private int passCount;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    lockonTime = 0;
    //    passCount = 0;
    //    Lockon = false;
    //    maxLockonTime = 1;
    //    todayTaskUI = GameObject.Find("TodayTask");
    //    todayTask = todayTaskUI.GetComponent<TodayTask>();
    //    target = GameObject.FindGameObjectWithTag("Target");
    //    heartBeat = GameObject.FindGameObjectWithTag("HeartBeat").GetComponent<HeartBeat>();
    //}

    //private void FixedUpdate()
    //{
    //    if (!Lockon)
    //    {
    //        RaycastHit hit;
    //        int layer = 1 << LayerMask.NameToLayer("Target");
    //        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 100, layer))
    //        {
    //            //レイにヒットしたオブジェクトがターゲットならば
    //            //if (hit.collider.gameObject.CompareTag("Target"))
    //            {

    //                Debug.Log("ハートが鼓動中です");
    //                heartBeat.FastHeartBeat();

    //                lockonTime += Time.deltaTime;

    //                if (lockonTime >= maxLockonTime)
    //                {
    //                    Debug.Log("ロックオン");

    //                    //ここから下の処理は一回でも実行したらそれ以降実行しない
    //                    if(passCount >= 1)
    //                    {
    //                        return;
    //                    }

    //                    for (int i = 0; i < todayTask.todayTask.Count; i++)
    //                    {
    //                        //タスクが1日目のタスク内容であれば
    //                        if (GameManager.Instance.tasks[i].date == 0)
    //                        {
    //                            GameManager.Instance.tasks[i].isCompletion = true;
    //                        }
    //                    }

    //                    int j = 0;

    //                    //もし、今のDayが1日目であれば
    //                    if (GameManager.Instance.GetDate() == 0)
    //                    {
    //                        //ロックオンを行う
    //                        Lockon = true;

    //                        //2日目に移行
    //                        Day1.day1 = true;
    //                    }

    //                    //もし、今のDayが2日目であれば
    //                    if (GameManager.Instance.GetDate() == 1)
    //                    {
    //                        //カメラのロックオンは行わない
    //                        Lockon = false;

    //                        //その日のタスクが全てクリアされていたら次のDayに移行
    //                        if (GameManager.Instance.tasks[j].isCompletion && GameManager.Instance.tasks[j + 1].isCompletion)
    //                        {
    //                            //3日目に移行
    //                            Day2.day2 = true;
    //                        }
    //                    }

    //                    //パスカウントをインクリメントする
    //                    passCount++;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("他のものに当たってます");
    //            heartBeat.IdleHeartBeat();
    //            lockonTime = 0.0f;
    //        }
    //    }
    //}
}
