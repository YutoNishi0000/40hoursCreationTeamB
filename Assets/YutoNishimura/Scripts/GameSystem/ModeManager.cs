using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public void SetModeManager(GameManager.GameMode mode)
    {
        GameManager.Instance.SetGameMode(mode);
    }
}
