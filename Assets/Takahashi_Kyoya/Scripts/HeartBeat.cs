using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBeat : MonoBehaviour
{
    //Å¬‚Ì‘å‚«‚³
    private Vector3 minSize = new Vector3(1.0f, 1.0f, 1.0f);
    //Å‘å‚Ì‘å‚«‚³
    private Vector3 maxSize = new Vector3(2.0f, 2.0f, 2.0f);
    //‘å‚«‚­‚È‚Á‚Ä‚¢‚é‚©
    private bool isBig = false;

    private void Update()
    {
        if (this.transform.localScale.x >= 1.9999)
        {
            isBig = false;
        }
        if (this.transform.localScale.x <= 1.01)
        {
            isBig = true;
        }

        if (isBig)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, maxSize, 0.05f);
        }
        else
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, minSize, 0.05f);
        }
    }
}
