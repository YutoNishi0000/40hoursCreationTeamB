using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : MonoBehaviour
{
    private const int childNum = 4;     //子オブジェクトの数
    private GameObject[] poseUI = new GameObject[childNum]; //UIのオブジェクト情報
    private bool IsPosing = false;
    void Start()
    {
        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i] = transform.GetChild(i).gameObject;
        }
        hideUI();
    }

    // Update is called once per frame
    void Update()
    {
        // ===== ポーズ中の処理 =====
        if (IsPosing)
        {
            showUI();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsPosing = false;
            }
            return;
        }
        // ===== ポーズじゃないときの処理 =====
        hideUI();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsPosing = true;
        }
    }
    private void hideUI()
    {
        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i].SetActive(false);
        }
    }
    private void showUI()
    {
        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i].SetActive(true);
        }
    }
}
