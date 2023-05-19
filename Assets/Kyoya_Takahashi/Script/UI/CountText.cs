using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountText : MonoBehaviour
{
    public void updateText(int n, Text t)
    {
        t.text = n.ToString().PadLeft(2, '0');
    }

}
