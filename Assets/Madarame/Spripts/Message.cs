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
        new MessageData{ massage = "うぅ...", color = Color.magenta },
        new MessageData{ massage = "早く”あの人”を見つけなくちゃ…", color = Color.magenta },
        new MessageData{ massage = "でも気づかれないようにしないと…", color = Color.magenta },
        new MessageData{ massage = "だって私は…", color = Color.magenta },
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