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
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isPoseing = !isPoseing;
            PoseCanvas.SetActive(isPoseing);
            //Player.SetActive(true);
            Message.PlayerMoveFlag = !Message.PlayerMoveFlag;
        }
    }
    public void ClickCalled()
    {
        GameManager.Instance.OutGameNextScene("Title");
    }
}
