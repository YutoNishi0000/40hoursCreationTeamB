using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedNegotiationEvent : MonoBehaviour
{
    Message message;
    [SerializeField] GameObject MessgeUI;

    //���񉟂��ꂽ�Ƃ��ɃJ������؂�ւ��邩
    const int count = 5;
    //�J�E���^�[
    int counter = 0;
    public static bool _escaprFromPlayer;                          //�v���C���[���瓦���铮������s���邩�ǂ���

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

    //�Q�b�^�[
    public bool GetEscapeFlag()
    {
        return _escaprFromPlayer;
    }
}
