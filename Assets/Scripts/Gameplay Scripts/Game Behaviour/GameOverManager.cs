using UnityEngine;
using PlayerLogic;

public class GameOverManager : MonoBehaviour
{
    [Header("-----Player Awards-----")]
    [SerializeField]
    private int qtyEarnedForWin = 5;

    [SerializeField]
    private int qtyEarnedForLoss = 2;
    
    [SerializeField]
    private int qtyEarnedForDraw = 3;

    private PlayerStats[] allPlayers;
    private PlayerStats playerWhoWon, playerWhoLost;

    private void OnEnable()
    {
        ProcessGameOver();
    }

    void ProcessGameOver() 
    {
        playerWhoWon = GetPlayerWhoWon();
        playerWhoLost = GetPlayerWhoLost();
        allPlayers = GetAllPlayers();

        if (!playerWhoWon)
        {
            //Draw Game, no players Alive
            AwardPlayer(allPlayers[0], qtyEarnedForDraw);
            AwardPlayer(allPlayers[1], qtyEarnedForDraw);

            UI_ManagerGameplay.Instance.SetGameOverTitleText(Translate.GetTranslatedText("draw"));
            return;
        }

        AwardPlayer(playerWhoWon, qtyEarnedForWin);
        AwardPlayer(playerWhoLost, qtyEarnedForLoss);

        CountVictory(playerWhoWon);
    }

    PlayerStats[] GetAllPlayers()
    {
        return FindObjectsOfType<PlayerStats>();
    }

    PlayerStats GetPlayerWhoWon()
    {
        PlayerStats[] availablePlayers = FindObjectsOfType<PlayerStats>();

        foreach (PlayerStats playerToCheckIfAlive in availablePlayers)
        {
            if (!playerToCheckIfAlive.isPlayerDead)
                return playerToCheckIfAlive;
        }

        return null;
    }

    PlayerStats GetPlayerWhoLost()
    {
        PlayerStats[] availablePlayers = FindObjectsOfType<PlayerStats>();

        foreach (PlayerStats playerToCheckIfDead in availablePlayers)
        {
            if (playerToCheckIfDead.isPlayerDead)
                return playerToCheckIfDead;
        }

        return null;
    }

    void AwardPlayer(PlayerStats playerToAward, int coinsToAward)
    {
        Economy.AddCoinsToPlayer(playerToAward.PlayerID, coinsToAward);

        if (playerToAward.PlayerID == 0)
        {
            FillUIPlayer0(coinsToAward);
            return;
        }

        FillUIPlayer1(coinsToAward);
    }

    void FillUIPlayer0(int qtyCoinsEarned)
    {
        string textToSet = "+ " + qtyCoinsEarned;

        UI_ManagerGameplay.Instance.SetPlayer0AwardText(textToSet);
    }

    void FillUIPlayer1(int qtyCoinsEarned)
    {
        string textToSet = "+ " + qtyCoinsEarned;

        UI_ManagerGameplay.Instance.SetPlayer1AwardText(textToSet);
    }

    void CountVictory(PlayerStats playerWinner)
    {
        string winnerTextToSet = VictoryText();

        int newVictoryAmmout = PlayersDataManager.PlayerVictoryCount(playerWinner.PlayerID) + 1;

        PlayersDataManager.SetNewPlayerVictoryAmmout(playerWinner.PlayerID, newVictoryAmmout);
        UI_ManagerGameplay.Instance.SetGameOverTitleText(winnerTextToSet);
    }

    string VictoryText() 
    {
        return "Player " + (playerWhoWon.PlayerID + 1) + " " + Translate.GetTranslatedText("won");
    }
}