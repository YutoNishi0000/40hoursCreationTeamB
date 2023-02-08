using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    //シーンのステート
    enum SCENE
    {
        title,
        ingame,
        gameover,
        gameclear,
    }
    SCENE scene;
    //BGM
    [SerializeField]List<AudioClip> audioClips = new List<AudioClip>();

    void Start()
    {
        switch(scene)
        {
            case SCENE.title:
                break;
            case SCENE.ingame:
                break;
            case SCENE.gameclear:
                break;
            case SCENE.gameover:
                break;
        }
    }

    void Update()
    {
        
    }
}
