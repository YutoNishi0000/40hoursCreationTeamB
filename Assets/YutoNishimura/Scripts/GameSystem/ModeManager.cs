using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public enum GameMode
    {
        Easy,
        Nomal,
        Hard
    }

    public static void SetModeManager(int mode)
    {
        switch(mode)
        {
            case (int)GameMode.Easy:
                GameManager.Instance.SetGameMode(GameManager.GameMode.Easy);
                break;
            case (int)GameMode.Nomal:
                GameManager.Instance.SetGameMode(GameManager.GameMode.Nomal);
                break;
            case (int)GameMode.Hard:
                GameManager.Instance.SetGameMode(GameManager.GameMode.Hard);
                break;
        }
    }
}
