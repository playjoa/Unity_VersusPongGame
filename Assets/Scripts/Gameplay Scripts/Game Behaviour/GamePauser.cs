using UnityEngine;
using PlayerLogic.Inputs;

public class GamePauser : MonoBehaviour
{
    void Update()
    {
        CheckForPause();
    }

    void CheckForPause()
    {
        if (PlayersInputs.PressedPause)
        {
            ProcessPauseRequest();
        }
    }

    void ProcessPauseRequest()
    {
        SoundManager.PlaySoundInList("click", 1);

        if (GameState.isPlaying())
        {
            GameState.SetState(GameStates.Paused);

            UI_ManagerGameplay.Instance.ToggleGamePlayScreen(false);
            UI_ManagerGameplay.Instance.TogglePauseScreen(true);
            return;
        }

        if (GameState.isPaused())
        {
            ResumeFromPause();
            UI_ManagerGameplay.Instance.TogglePauseScreen(false);
            UI_ManagerGameplay.Instance.ToggleGamePlayScreen(true);
        }
    }

    public void ResumeFromPause()
    {
        GameState.SetState(GameStates.Playing);
    }
}
