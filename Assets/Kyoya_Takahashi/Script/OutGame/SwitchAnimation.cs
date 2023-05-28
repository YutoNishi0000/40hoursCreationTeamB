using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimation : UIController
{
    private void OnEnable()
    {
        transform.parent = GameObject.Find("Animation").transform;
    }
    void AnimetionEnd()
    {
        Destroy(this.gameObject);
    }
}
