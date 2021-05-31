using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool HasSeenTutorial()
    {
        return PlayerPrefs.HasKey("tutorial");
    }

    void RegisterTutorialSeen() 
    {
        PlayerPrefs.SetInt("tutorial", 1);
    }

    public void InitializeTutorial()
    {
        UI_ManagerGameplay.Instance.ToggleGamePlayScreen(false);
        UI_ManagerGameplay.Instance.ToggleTutorialScreen(true);
    }

    public void ClickOutOfTutorial() 
    {
        RegisterTutorialSeen();
    }
}
