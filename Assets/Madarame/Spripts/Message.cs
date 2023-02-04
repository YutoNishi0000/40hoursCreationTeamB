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
        new MessageData{ massage = "うぅ...", color = Color.magenta },
        new MessageData{ massage = "早く”あの人”を見つけなくちゃ…", color = Color.magenta },
        new MessageData{ massage = "でも気づかれないようにしないと…", color = Color.magenta },
        new MessageData{ massage = "だって私は…", color = Color.magenta },
        //day2
        new MessageData{ massage = "今日もまず”あの人”を見つけるところから始めなきゃ…", color = Color.magenta },
        new MessageData{ massage = "あの人はどこに向かってるのかな…？", color = Color.magenta },
        new MessageData{ massage = "「お店から出てきたところ」を激写しちゃおう！", color = Color.magenta },
        new MessageData{ massage = "隠れながら写真を撮れる場所あるかな…", color = Color.magenta },
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