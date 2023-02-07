using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMove : MonoBehaviour
{
    [SerializeField] string SceneName;
    public void LoadScene()
    {
        //w’è•b”‘Ò‚Á‚Ä‚©‚çw’è‚µ‚½scene‚ÉˆÚ“®
        FadeManager.Instance.LoadScene(SceneName, 2.0f);
    }
}
