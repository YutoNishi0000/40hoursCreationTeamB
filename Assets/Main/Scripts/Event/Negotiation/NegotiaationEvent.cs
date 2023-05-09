using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiaationEvent : MonoBehaviour
{
    Message message;
    [SerializeField]GameObject MessgeUI;

    //���񉟂��ꂽ�Ƃ��ɃJ������؂�ւ��邩
    const int countCamere = 8;
    //���񉟂��ꂽ��BGM�Đ�����̂�
    const int countBGM = 18;
    //�J�E���^�[
    int counter = 0;
    //�J�����I�u�W�F�N�g
    [SerializeField] GameObject obj;


    private void Start()
    {
        message = MessgeUI.GetComponent<Message>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            counter++;
            if (counter == countCamere)
            {
                Debug.Log("�ʂ���");
                obj.gameObject.SetActive(true);
            }
            if(counter == countBGM)
            {
                SoundManager.Instance.PlayGameClearBGM();
                //GameManager.Instance.OutGameNextScene("Title");
                counter++;
            }
        }
    }
}
