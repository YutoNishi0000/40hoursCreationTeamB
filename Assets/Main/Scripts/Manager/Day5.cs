using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5 : MonoBehaviour
{
    //�^�[�Q�b�g�I�u�W�F�N�g
    [SerializeField] GameObject target;
    //�v���[���[�I�u�W�F�N�g
    [SerializeField] GameObject player;
    //�v���C���[�ƃ^�[�Q�b�g�̋���
    float distance = 0;
    //�^�[�Q�b�g�̔F���͈�
    const float area = 5;

    private TodayTask todayTask;

    //day5���I��肻�����ǂ���
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
        //    //�^�X�N����ł��c���Ă�����
        //    if (todayTask.todayTask.Count > 0)
        //    {
        //        Debug.Log("�g�D���[�G���h");
        //        GameManager.Instance.NextDay("SucsessNegotiation");
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        Debug.Log("�o�b�h�G���h");
        //        GameManager.Instance.NextDay("FailedNegotiation");
        //        Destroy(gameObject);
        //    }
        //}
    }
}
