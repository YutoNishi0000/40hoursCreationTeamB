using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UIの表示非表示を行うためのクラス
public class UIController : MonoBehaviour
{
    public static bool _talkStart;

    public Image[] OffImage;

    public Text[] OffTexts;

    private PlayerStateController _playerState;

    // Start is called before the first frame update
    void Start()
    {
        _playerState = GetComponent<PlayerStateController>();
        _talkStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ステートがトーク状態だったら
        if(_playerState.GetPlayerState() == PlayerStateController.PlayerState.TalkEvent)
        {
            Debug.Log("UI非表示");
            OffUI(OffImage, OffTexts);
        }
        //トーク状態以外だったら
        else
        {
            Debug.Log("UI表示");
            OnUI(OffImage, OffTexts);
        }
    }

    public void OffUI(Image[] images, Text[] texts)
    {
        for(int i = 0; i < images.Length; i++)
        {
            if (images == null)
            {
                break;
            }

            //配列に入っている全てのイメージUI、テキストUIを非表示にする
            images[i].enabled = false;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts == null)
            {
                break;
            }

            //配列に入っている全てのイメージUI、テキストUIを非表示にする
            images[i].enabled = false;
        }
    }

    public void OnUI(Image[] images, Text[] texts)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images == null)
            {
                break;
            }

            //配列に入っている全てのイメージUI、テキストUIを非表示にする
            images[i].enabled = true;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts == null)
            {
                break;
            }

            //配列に入っている全てのイメージUI、テキストUIを非表示にする
            images[i].enabled = true;
        }
    }
}
