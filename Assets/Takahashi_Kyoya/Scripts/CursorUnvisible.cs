using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUnvisible : MonoBehaviour
{
    //ingame�����ǂ���
    public static bool isIngame = false;
    void Start()
    {
        Cursor.visible = !isIngame;
    }
}
