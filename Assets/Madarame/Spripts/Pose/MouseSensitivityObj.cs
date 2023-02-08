using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityObj : MonoBehaviour
{
    private Slider MouseSlider;

    public void SetSensitivity(float sensitivity)
    {
        MouseSensitivity.SetSensitivity(sensitivity);
    }
    private void Start()
    {
        MouseSlider = GetComponent<Slider>();
        MouseSlider.value = MouseSensitivity.GetSensitivity();
    }
    private void Update()
    {
        Debug.Log(MouseSensitivity.GetSensitivity());
    }
}
