using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : MonoBehaviour
{
    [SerializeField] GameObject PoseCanvas;
    [SerializeField] GameObject Player;
    bool isPoseing = false;

    public bool GetIsPoseing() { return isPoseing; }

    private void Update()
    {
        if (!Player.activeSelf)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !PoseCanvas.activeSelf)
        {
            PoseCanvas.SetActive(true);
            Player.SetActive(false);
            isPoseing = true;
        }
    }
    public void OnClick()
    {
        PoseCanvas.SetActive(false);
        Player.SetActive(true);
        isPoseing = false;
    }
    public void ClickCalled()
    {
        GameManager.Instance.OutGameNextScene("Title");
    }
}
