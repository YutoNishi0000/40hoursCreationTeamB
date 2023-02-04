using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraAngle : Human
{
    public static bool _voyeurism;                        //“B‚·‚é‚©
    private CameraController cameraController;
    public Camera mainCamera;
    private Camera subCamera;

    // Start is called before the first frame update
    void Start()
    {
        _voyeurism = false;
        cameraController = GetComponentInChildren<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log("“Bƒ‚[ƒh‚Í‚¢‚è‚Ü‚µ‚½");
        }
    }

    public void ExitVoyeurism()
    {
        _voyeurism = false;
        playerInstance._moveLock = false;
        mainCamera.enabled = true;
        subCamera.enabled = false;
        Debug.Log("“Bƒ‚[ƒhI‚í‚è‚Ü‚µ‚½");
    }
}