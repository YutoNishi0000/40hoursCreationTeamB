using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraAngle : Human
{
    public static bool _voyeurism;                        //ìêéBÇ∑ÇÈÇ©
    private CameraController cameraController;
    public Camera mainCamera;
    private Camera subCamera;

    GameObject SE = null;

    // Start is called before the first frame update
    void Start()
    {
        _voyeurism = false;
        cameraController = GetComponentInChildren<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            subCamera.enabled = false;
            mainCamera.enabled = true;
            playerInstance._moveLock = false;
            //UIController._talkStart = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ShutterPoint"))
        {
            _voyeurism = true;
            playerInstance._moveLock = true;
            subCamera = other.GetComponent<Camera>();
            subCamera.enabled = true;
            mainCamera.enabled = false;
            Debug.Log("ìêéBÉÇÅ[ÉhÇÕÇ¢ÇËÇ‹ÇµÇΩ");
        }
        if (other.gameObject.CompareTag("Handkerchief"))
        {
            playerInstance._moveLock = true;
            subCamera = other.GetComponentInChildren<Camera>();
            subCamera.enabled = true;
            mainCamera.enabled = false;
        }
    }

    public void ExitVoyeurism()
    {
        _voyeurism = false;
        playerInstance._moveLock = false;
        mainCamera.enabled = true;
        subCamera.enabled = false;
        Debug.Log("ìêéBÉÇÅ[ÉhèIÇÌÇËÇ‹ÇµÇΩ");
    }
}