using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseSensitivity
{
    private static float Sensitivity = 1.0f;
    public static float GetSensitivity() { return Sensitivity; }
    public static void SetSensitivity(float sensitivity) { Sensitivity = sensitivity; }
}
