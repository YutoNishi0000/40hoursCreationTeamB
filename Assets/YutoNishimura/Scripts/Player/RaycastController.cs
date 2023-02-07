using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public static float lockonTime = 0.0f;
    private float maxLockonTime;
    public static bool Lockon;
    public static bool BeatHeart;
    private bool _lock;

    //ターゲット
    GameObject target = null;
    //
    GameObject todayTaskUI = null;
    TodayTask todayTask = null; 
    // Start is called before the first frame update
    void Start()
    {
        _lock = false;
        lockonTime = 0;
        Lockon = false;
        BeatHeart = false;
        maxLockonTime = 1;
        todayTaskUI = GameObject.Find("TodayTask");
        todayTask = todayTaskUI.GetComponent<TodayTask>();
        target = GameObject.FindGameObjectWithTag("Target");
    }

    private void FixedUpdate()
    {
        Debug.Log("ロックオンの状態はぁぁぁぁl" + Lockon);
        if (!Lockon)
        {
            RaycastHit hit;
            int layer = 1 << LayerMask.NameToLayer("Target");
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 100, layer))
            {
                //レイにヒットしたオブジェクトがターゲットならば
                //if (hit.collider.gameObject.CompareTag("Target"))
                {
                    BeatHeart = true;

                    Debug.Log("ハートが鼓動中です");

                    //=============================================================================
                    //
                    Debug.Log("今の日にちはぁぁぁぁヵ" + GameManager.Instance.GetDate());
                    if (GameManager.Instance.GetDate() != 0)
                    {
                        return;
                    }
                    //
                    //=============================================================================

                    lockonTime += Time.deltaTime;
                    if (lockonTime >= maxLockonTime)
                    {
                        Debug.Log("ロックオン");
                        Lockon = true;
                        todayTask.TaskCompletion(0);
                        Day1.day1 = true;
                    }
                }
            }
            else
            {
                Debug.Log("他のものに当たってます");
                lockonTime = 0.0f;
                BeatHeart = false;
            }
        }
    }
}
