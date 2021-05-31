public static class GameState
{
    private static GameStates currentState = GameStates.None;

    public static GameStates GetState() => currentState;

    public static void SetState(GameStates newState)
    {
        currentState = newState;
    }

    public static bool isPaused()
    {
        if (GetState() == GameStates.Paused)
            return true;

        return false;
    }

    public static bool isPlaying()
    {
        if (GetState() == GameStates.Playing)
            return true;

        return false;
    }

}
public enum GameStates
{
    None,
    WaitingForStart,
    Playing,
    Paused,
    Finished
}

