using UnityEngine;
using System.Collections;
using PlayerLogic;
using PlayerLogic.Actions;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TutorialManager tutorialManager;

    private int totalPlayersQty;

    private static int currentPlayer = 0;

    private WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);
    private WaitForSeconds waitABitToStart = new WaitForSeconds(0.75f);

    private void OnEnable()
    {
        PlayerShooter.OnPlayerShot += PlayerShot;
        PlayerHealth.OnPlayerDied += PlayerDied;
    }

    private void OnDisable()
    {
        PlayerShooter.OnPlayerShot -= PlayerShot;
        PlayerHealth.OnPlayerDied -= PlayerDied;
    }

    private void Start()
    {
        PrepareGame();
    }

    void PrepareGame()
    {
        GameState.SetState(GameStates.WaitingForStart);

        GetTotalPlayersInGame();

        if (!tutorialManager.HasSeenTutorial())
        {
            tutorialManager.InitializeTutorial();
            return;
        }

        RequestStartGame();
    }

    public void RequestStartGame()
    {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        yield return waitABitToStart;

        for (int countDown = 3; countDown >= 0; countDown--)
        {
            string countDownValue = countDown + "!";

            if (countDown == 0)
                countDownValue = Translate.GetTranslatedText("go");

            UI_ManagerGameplay.Instance.ToggleAndSetCountdownText(countDownValue);
            yield return waitForOneSecond;
        }

        InitializeGame();
    }

    void InitializeGame()
    {
        GameState.SetState(GameStates.Playing);
        currentPlayer = 0;
    }

    void GetTotalPlayersInGame()
    {
        totalPlayersQty = FindObjectsOfType<PlayerStats>().Length;
    }

    public static bool isPlayersTurn(PlayerStats playerToCheck)
    {
        if (!GameState.isPlaying())
            return false;

        if (playerToCheck.PlayerID == currentPlayer)
            return true;

        return false;
    }

    void FinishGame()
    {
        UI_ManagerGameplay.Instance.ToggleGamePlayScreen(false);
        UI_ManagerGameplay.Instance.ToggleGameOverScreen(true);

        GameState.SetState(GameStates.Finished);
    }

    int GetNextPlayer()
    {
        int nextPlayerID = currentPlayer;

        nextPlayerID++;

        if (nextPlayerID >= totalPlayersQty)
            nextPlayerID = 0;

        return nextPlayerID;
    }

    void PlayerShot(PlayerStats playerWhoShot)
    {
        if (!GameState.isPlaying())
            return;

        currentPlayer = GetNextPlayer();
    }

    void PlayerDied(PlayerStats playerWhoDied)
    {
        FinishGame();
    }
}