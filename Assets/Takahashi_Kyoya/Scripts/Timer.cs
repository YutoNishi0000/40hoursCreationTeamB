using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]private Text secondUI = null;
    [SerializeField]private Text minuteUI = null;
    //êßå¿éûä‘
    public float timeLimit = 120.0f;

    private void Update()
    {
        TextUpdate();
    }
    void TextUpdate()
    {
        timeLimit -= Time.deltaTime;
        secondUI.text = ((int)timeLimit % 60.0f).ToString("00");
        minuteUI.text = ((int)(timeLimit / 60)).ToString("00");
    }
}
