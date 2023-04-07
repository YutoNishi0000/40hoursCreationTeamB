using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] CoolTime coolTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            coolTime.StartCoolTime();
        }
        if(coolTime.IsEnable)
        {
            Debug.Log("Žg—p‰Â”\");
        }
    }
}
