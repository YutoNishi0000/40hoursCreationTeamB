using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    //public static float lockonTime = 0.0f;
    //private float maxLockonTime;
    //public static bool Lockon;

    ////�^�[�Q�b�g
    //GameObject target = null;
    ////
    //GameObject todayTaskUI = null;
    //TodayTask todayTask = null; 

    ////�����A�j���[�V����
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
    //            //���C�Ƀq�b�g�����I�u�W�F�N�g���^�[�Q�b�g�Ȃ��
    //            //if (hit.collider.gameObject.CompareTag("Target"))
    //            {

    //                Debug.Log("�n�[�g���ۓ����ł�");
    //                heartBeat.FastHeartBeat();

    //                lockonTime += Time.deltaTime;

    //                if (lockonTime >= maxLockonTime)
    //                {
    //                    Debug.Log("���b�N�I��");

    //                    //�������牺�̏����͈��ł����s�����炻��ȍ~���s���Ȃ�
    //                    if(passCount >= 1)
    //                    {
    //                        return;
    //                    }

    //                    for (int i = 0; i < todayTask.todayTask.Count; i++)
    //                    {
    //                        //�^�X�N��1���ڂ̃^�X�N���e�ł����
    //                        if (GameManager.Instance.tasks[i].date == 0)
    //                        {
    //                            GameManager.Instance.tasks[i].isCompletion = true;
    //                        }
    //                    }

    //                    int j = 0;

    //                    //�����A����Day��1���ڂł����
    //                    if (GameManager.Instance.GetDate() == 0)
    //                    {
    //                        //���b�N�I�����s��
    //                        Lockon = true;

    //                        //2���ڂɈڍs
    //                        Day1.day1 = true;
    //                    }

    //                    //�����A����Day��2���ڂł����
    //                    if (GameManager.Instance.GetDate() == 1)
    //                    {
    //                        //�J�����̃��b�N�I���͍s��Ȃ�
    //                        Lockon = false;

    //                        //���̓��̃^�X�N���S�ăN���A����Ă����玟��Day�Ɉڍs
    //                        if (GameManager.Instance.tasks[j].isCompletion && GameManager.Instance.tasks[j + 1].isCompletion)
    //                        {
    //                            //3���ڂɈڍs
    //                            Day2.day2 = true;
    //                        }
    //                    }

    //                    //�p�X�J�E���g���C���N�������g����
    //                    passCount++;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("���̂��̂ɓ������Ă܂�");
    //            heartBeat.IdleHeartBeat();
    //            lockonTime = 0.0f;
    //        }
    //    }
    //}
}
