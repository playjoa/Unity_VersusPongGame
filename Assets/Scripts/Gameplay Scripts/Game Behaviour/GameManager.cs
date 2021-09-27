using UnityEngine;
using System.Collections;
using PlayerLogic;
using PlayerLogic.Actions;
using TranslationSystem.Base;

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

    private void PrepareGame()
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

    private IEnumerator StartSequence()
    {
        yield return waitABitToStart;

        for (var countDown = 3; countDown >= 0; countDown--)
        {
            var countDownValue = countDown + "!";

            if (countDown == 0)
                countDownValue = Translate.GetTranslatedText("go");

            UI_ManagerGameplay.Instance.ToggleAndSetCountdownText(countDownValue);
            yield return waitForOneSecond;
        }

        InitializeGame();
    }

    private void InitializeGame()
    {
        GameState.SetState(GameStates.Playing);
        currentPlayer = 0;
    }

    private void GetTotalPlayersInGame()
    {
        totalPlayersQty = FindObjectsOfType<PlayerStats>().Length;
    }

    public static bool IsPlayersTurn(PlayerStats playerToCheck)
    {
        if (!GameState.isPlaying())
            return false;

        return playerToCheck.PlayerID == currentPlayer;
    }

    private void FinishGame()
    {
        UI_ManagerGameplay.Instance.ToggleGamePlayScreen(false);
        UI_ManagerGameplay.Instance.ToggleGameOverScreen(true);

        GameState.SetState(GameStates.Finished);
    }

    private int GetNextPlayer()
    {
        var nextPlayerID = currentPlayer;

        nextPlayerID++;

        if (nextPlayerID >= totalPlayersQty)
            nextPlayerID = 0;

        return nextPlayerID;
    }

    private void PlayerShot(PlayerStats playerWhoShot)
    {
        if (!GameState.isPlaying())
            return;

        currentPlayer = GetNextPlayer();
    }

    private void PlayerDied(PlayerStats playerWhoDied)
    {
        FinishGame();
    }
}