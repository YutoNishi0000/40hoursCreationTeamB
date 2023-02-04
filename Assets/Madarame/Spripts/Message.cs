using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageData
{
    public string massage { get; set; } 
    public Color color { get; set; }
}
public class Message : MonoBehaviour
{
    public Text messageText;
    public GameObject Panel;
    private int count = 0;
    [SerializeField] private Text text;

    List<MessageData> meslist = new List<MessageData>()
    {
        //day1
        new MessageData{ massage = "����...", color = Color.magenta },
        new MessageData{ massage = "�����h���̐l�h�������Ȃ�����c", color = Color.magenta },
        new MessageData{ massage = "�ł��C�Â���Ȃ��悤�ɂ��Ȃ��Ɓc", color = Color.magenta },
        new MessageData{ massage = "�����Ď��́c", color = Color.magenta },
        //day2
        new MessageData{ massage = "�������܂��h���̐l�h��������Ƃ��납��n�߂Ȃ���c", color = Color.magenta },
        new MessageData{ massage = "���̐l�͂ǂ��Ɍ������Ă�̂��ȁc�H", color = Color.magenta },
        new MessageData{ massage = "�u���X����o�Ă����Ƃ���v�����ʂ����Ⴈ���I", color = Color.magenta },
        new MessageData{ massage = "�B��Ȃ���ʐ^���B���ꏊ���邩�ȁc", color = Color.magenta },
    };

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(meslist.Count > count)
            {
                SetMes(count);
                count++;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void SetMes(int num)
    {
        text.text = meslist[num].massage;
        text.color = meslist[num].color;
    }
}