using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : MonoBehaviour
{
    private const int childNum = 4;     //�q�I�u�W�F�N�g�̐�
    private GameObject[] poseUI = new GameObject[childNum]; //UI�̃I�u�W�F�N�g���
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
        // ===== �|�[�Y���̏��� =====
        if (IsPosing)
        {
            showUI();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsPosing = false;
            }
            return;
        }
        // ===== �|�[�Y����Ȃ��Ƃ��̏��� =====
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
