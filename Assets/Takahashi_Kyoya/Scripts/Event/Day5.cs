using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5 : MonoBehaviour
{
    //day5Ç™èIÇÌÇËÇªÇ§Ç©Ç«Ç§Ç©
    public static bool day5 = false;
    // Start is called before the first frame update
    void Start()
    {
        day5 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(day5)
        {
            GameManager.Instance.GameClear();
            day5 = false;
        }
    }
}
