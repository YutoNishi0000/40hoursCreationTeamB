using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeScore : ScoreManger
{
    //�J�����̃N�[���^�C��
    private float coolTime = 3;
    //�J�����g�p�\��
    private bool cameraEnable = true;

    private ScreenShot screen;
    //[SerializeField] private Image getTimeImg;


    private void Start()
    {
        screen = GameObject.FindObjectOfType<ScreenShot>();
    }
    private void LateUpdate()
    {
        if (Shutter.isFilming)
        {


            //screen.FadeIn(getTimeImg);
        }
    }
}

