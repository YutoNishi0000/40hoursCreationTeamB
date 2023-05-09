using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiaationEvent : MonoBehaviour
{
    Message message;
    [SerializeField]GameObject MessgeUI;

    //何回押されたときにカメラを切り替えるか
    const int countCamere = 8;
    //何回押されたらBGM再生するのか
    const int countBGM = 18;
    //カウンター
    int counter = 0;
    //カメラオブジェクト
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
                Debug.Log("通った");
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
