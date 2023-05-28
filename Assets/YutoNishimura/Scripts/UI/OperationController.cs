using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OperationController : MonoBehaviour
{
    private const int sceneIndex = 1;  //ÉzÅ[ÉÄÇ…ñﬂÇÈ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
