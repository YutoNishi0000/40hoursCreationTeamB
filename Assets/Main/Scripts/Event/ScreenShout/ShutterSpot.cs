using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterSpot : MonoBehaviour
{
    public GameObject[] shutterObj = null;
    private TodayTask todayTask;

    // Start is called before the first frame update
    void Start()
    {
        //todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();

        ////������Day2�̃^�X�N���I����Ă��Ȃ����
        //for (int i = 0; i < todayTask.todayTask.Count; i++)
        //{
        //    if (todayTask.todayTask[i] == GameManager.Instance.GetTaskName(GameManager.Instance.GetDate() - 1))
        //    {
        //        for(int j = 0; j < shutterObj.Length; j++)
        //        {
        //            shutterObj[j].SetActive(false);
        //        }
        //    }
        //}
    }
}
