using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMove : MonoBehaviour
{
    [SerializeField] string SceneName;
    public void LoadScene()
    {
        //�w��b���҂��Ă���w�肵��scene�Ɉړ�
        FadeManager.Instance.LoadScene(SceneName, 2.0f);
    }
}
