using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiaationEvent : MonoBehaviour
{
    Message message;
    [SerializeField]GameObject MessgeUI;

    //何回押されたときにカメラを切り替えるか
    const int count = 8;
    //カウンター
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
