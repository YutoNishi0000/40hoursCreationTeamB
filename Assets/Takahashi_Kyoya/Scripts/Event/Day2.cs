using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2 : MonoBehaviour
{
    public static bool day2 = false;
    //�^�C�}�[
    float timer = 0;
    //�t�F�C�h�A�E�g����܂ł̎���
    float faidOutTime = 3;
    void Update()
    {
        if (day2)
        {
            timer += Time.deltaTime;
            if (timer > faidOutTime)
            {
                GameManager.Instance.NextDay("Day 3_k");
                Destroy(gameObject);
            }
        }
    }
}
