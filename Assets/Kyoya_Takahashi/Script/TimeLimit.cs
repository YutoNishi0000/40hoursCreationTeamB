using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimit : MonoBehaviour
{
    float time = 1.8f;
    private void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
