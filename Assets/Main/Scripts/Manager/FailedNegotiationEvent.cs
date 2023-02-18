using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedNegotiationEvent : MonoBehaviour
{
    Message message;
    [SerializeField] GameObject MessgeUI;

    //何回押されたときにカメラを切り替えるか
    const int count = 5;
    //カウンター
    int counter = 0;
    public static bool _escaprFromPlayer;                          //プレイヤーから逃げる動作を実行するかどうか

    // Start is called before the first frame update
    void Start()
    {
        _escaprFromPlayer = false;
        message = MessgeUI.GetComponent<Message>();
        message.DrawGO_Text();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            counter++;
            if (counter == count)
            {
                this.gameObject.SetActive(false);
                _escaprFromPlayer = true;
            }
        }
    }

    //ゲッター
    public bool GetEscapeFlag()
    {
        return _escaprFromPlayer;
    }
}
