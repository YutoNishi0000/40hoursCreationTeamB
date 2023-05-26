using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        transform.parent = GameObject.Find("Animation").transform;
    }
    public void DestroyAnimation()
    {
        Destroy(this.gameObject);
    }
}
