using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistController : MonoBehaviour
{
    //�Q�[���I�����ɌĂяo��
    public void EndGame()
    {
        GameManager.Instance.GameQuit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}
