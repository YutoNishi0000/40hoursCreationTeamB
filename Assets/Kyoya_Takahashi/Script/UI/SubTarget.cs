using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubTarget : CountText
{
    Text text = null;
    void Start()
    {
        text = this.GetComponent<Text>();
    }
    void Update()
    {
        updateText(GameManager.Instance.numSubShutter, text);
    }
}
