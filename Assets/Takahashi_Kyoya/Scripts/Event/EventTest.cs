using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    [SerializeField] GameObject Todaytask;
    TodayTask todayTask;
    private void Awake()
    {
        todayTask = Todaytask.GetComponent<TodayTask>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            todayTask.TaskCompletion(1);
        }
    }
}
