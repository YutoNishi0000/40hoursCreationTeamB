using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//会話シーンと移動シーンでカメラを切り替えたい
public class CameraController : Human
{
    private Camera mainCamera;
    public Camera subCamera;
    private PlayerStateController playerState;
    private bool change;

    private void Start()
    {
        change = false;
        mainCamera = GetComponent<Camera>();
        playerState = GetComponentInParent<PlayerStateController>();
    }

    private void LateUpdate()
    {
        if (playerState.GetPlayerState() == PlayerStateController.PlayerState.Voyeurism)
        {
            mainCamera.enabled = false;
            subCamera.enabled = true;
            //subCamera.SetActive(true);
        }
        else
        {
            mainCamera.enabled = true;

            if (change)
            {
                subCamera.enabled = false;
            }
        }
    }

    public void SetCamera(Camera camera)
    {
        subCamera = camera;
        change = true;
    }
}
