using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiaationEvent : MonoBehaviour
{
    Message message;
    [SerializeField]GameObject MessgeUI;

    //���񉟂��ꂽ�Ƃ��ɃJ������؂�ւ��邩
    const int count = 8;
    //�J�E���^�[
    int counter = 0;


    private void Start()
    {
        message = MessgeUI.GetComponent<Message>();
        message.DrawGC_Text();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            counter++;
            if (counter == count)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
