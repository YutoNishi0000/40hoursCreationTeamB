using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTarget : CountText
{
    Text text = null;
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        updateText(GameManager.Instance.numTargetShutter, text);
    }
}
