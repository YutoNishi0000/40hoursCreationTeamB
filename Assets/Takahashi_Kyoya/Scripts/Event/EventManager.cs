using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //�����̃^�X�N�̏������Ă�I�u�W�F�N�g�ƃX�N���v�g
    GameObject task;
    TodayTask todayTask;
    private void Start()
    {
        task = GameObject.Find("TodayTask");
        todayTask = task.GetComponent<TodayTask>();
    }
    //�Q�[���I���Ƃ��̔���
}
