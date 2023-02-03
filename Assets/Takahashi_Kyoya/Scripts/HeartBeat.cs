using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBeat : MonoBehaviour
{
    //大きくなるスピード
    [SerializeField] private float largeSpeed;
    //小さくなるスピード
    [SerializeField] private float smallSpeed;
    //最小の大きさ
    private Vector3 minSize = new Vector3(1.0f, 1.0f, 1.0f);
    //最大の大きさ
    private Vector3 maxSize = new Vector3(2.0f, 2.0f, 2.0f);
    //大きくなっているか
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
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, maxSize, 0.01f * largeSpeed);
        }
        else
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, minSize, 0.01f * smallSpeed);
        }
    }
}
