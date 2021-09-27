using System.Linq;
using UnityEngine;
using PlayerLogic;
using TranslationSystem.Base;

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

   private void ProcessGameOver() 
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

    private PlayerStats[] GetAllPlayers()
    {
        return FindObjectsOfType<PlayerStats>();
    }

    private PlayerStats GetPlayerWhoWon()
    {
        var availablePlayers = FindObjectsOfType<PlayerStats>();

        return availablePlayers.FirstOrDefault(playerToCheckIfAlive => !playerToCheckIfAlive.isPlayerDead);
    }

    private PlayerStats GetPlayerWhoLost()
    {
        var availablePlayers = FindObjectsOfType<PlayerStats>();

        return availablePlayers.FirstOrDefault(playerToCheckIfDead => playerToCheckIfDead.isPlayerDead);
    }

    private void AwardPlayer(PlayerStats playerToAward, int coinsToAward)
    {
        Economy.AddCoinsToPlayer(playerToAward.PlayerID, coinsToAward);

        if (playerToAward.PlayerID == 0)
        {
            FillUIPlayer0(coinsToAward);
            return;
        }

        FillUIPlayer1(coinsToAward);
    }

    private void FillUIPlayer0(int qtyCoinsEarned)
    {
        var textToSet = "+ " + qtyCoinsEarned;

        UI_ManagerGameplay.Instance.SetPlayer0AwardText(textToSet);
    }

    private void FillUIPlayer1(int qtyCoinsEarned)
    {
        var textToSet = "+ " + qtyCoinsEarned;

        UI_ManagerGameplay.Instance.SetPlayer1AwardText(textToSet);
    }

    private void CountVictory(PlayerStats playerWinner)
    {
        var winnerTextToSet = VictoryText;

        var newVictoryAmount = PlayersDataManager.PlayerVictoryCount(playerWinner.PlayerID) + 1;

        PlayersDataManager.SetNewPlayerVictoryAmmout(playerWinner.PlayerID, newVictoryAmount);
        UI_ManagerGameplay.Instance.SetGameOverTitleText(winnerTextToSet);
    }

    private string VictoryText => Translate.GetTranslatedText("player") + " " + (playerWhoWon.PlayerID + 1) + " " +
                                  Translate.GetTranslatedText("won");
}