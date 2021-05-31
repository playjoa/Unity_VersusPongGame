using System;
using EconomyTools;

public static class Economy
{
    public static Action OnCoinValueChange;

    public static PlayerCoins[] playerCoins = new PlayerCoins[] { new PlayerCoins(0), new PlayerCoins(1) };

    public static string ListTotalCoinsPlayer(int playerID)
    {
        if (IsIDInvalid(playerID))
            return "";

        return playerCoins[playerID].ListTotalCoins();
    }

    public static bool CheckIfPlayerHasCoins(int playerID, int valueToCheck)
    {
        if (IsIDInvalid(playerID))
            return false;

        return playerCoins[playerID].CheckIfHasCoins(valueToCheck);
    }

    public static bool ChargePlayer(int playerID, int valueToCharge)
    {
        if (IsIDInvalid(playerID))
            return false;

        if (playerCoins[playerID].ChargeCoins(valueToCharge)) 
        {
            OnCoinValueChange?.Invoke();
            return true;
        }

        return false;
    }
    
    public static void AddCoinsToPlayer(int playerID, int valueToAdd) 
    {
        if (IsIDInvalid(playerID))
            return;

        playerCoins[playerID].AddCoins(valueToAdd);
        OnCoinValueChange?.Invoke();
    }

    static bool IsIDInvalid(int desiredID)
    {
        if (desiredID < 0)
            return true;

        if (desiredID >= playerCoins.Length)
            return true;

        return false;
    }
}