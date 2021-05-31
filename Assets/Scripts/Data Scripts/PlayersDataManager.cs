using PlayerDataSystem;
using SaveLoadSystem;

public static class PlayersDataManager
{
    private const int ammountOfPlayersDataToCreate = 2;
    private static PlayersData allPlayersData  = null;

    public static void CreateFirstTimeDataAndSave()
    {
        PlayerData[] playersList = new PlayerData[ammountOfPlayersDataToCreate];

        for (int i = 0; i < ammountOfPlayersDataToCreate; i++)
        {
            PlayerData tempPlayerData = new PlayerData();
            tempPlayerData.playerID = i;
            playersList[i] = tempPlayerData;
        }

        allPlayersData = PlayerDataConstructor(playersList);

        DataStorer.SaveData(allPlayersData);
    }

    static PlayersData PlayerDataConstructor(PlayerData[] dataArray)
    {
        PlayersData tempPlayersData = new PlayersData();
        tempPlayersData.playersDataArray = dataArray;
        return tempPlayersData;
    }

    static void RequestLoadDataIfNotSet() 
    {
        if (allPlayersData != null)
            return;

        allPlayersData = DataStorer.LoadData();
    }

    public static int PlayerMoneyAmmout(int playerID) 
    {
        RequestLoadDataIfNotSet();
        return allPlayersData.playersDataArray[playerID].moneyAmmount;
    }

    public static void SetNewPlayerMoneyAmmout(int playerID, int newValueToSet)
    {
        RequestLoadDataIfNotSet();
        allPlayersData.playersDataArray[playerID].moneyAmmount = newValueToSet;
        DataStorer.SaveData(allPlayersData);
    }

    public static int PlayerVictoryCount(int playerID)
    {
        RequestLoadDataIfNotSet();
        return allPlayersData.playersDataArray[playerID].victoryCount;
    }

    public static void SetNewPlayerVictoryAmmout(int playerID, int newValueToSet)
    {
        RequestLoadDataIfNotSet();
        allPlayersData.playersDataArray[playerID].victoryCount = newValueToSet;
        DataStorer.SaveData(allPlayersData);
    }

    public static bool PlayerHasBulletItem(int playerID, int bulletIndexToCheck)
    {
        RequestLoadDataIfNotSet();
        return allPlayersData.playersDataArray[playerID].hasBulletItem[bulletIndexToCheck];
    }

    public static void SetNewPlayerNewBulletItemValue(int playerID, int bulletIndex, bool newValueToSet)
    {
        RequestLoadDataIfNotSet();
        allPlayersData.playersDataArray[playerID].hasBulletItem[bulletIndex] = newValueToSet;
        DataStorer.SaveData(allPlayersData);
    }

    public static int PlayerCurrentBulletLoaded(int playerID)
    {
        RequestLoadDataIfNotSet();
        return allPlayersData.playersDataArray[playerID].currentBulletLoaded;
    }

    public static void SetNewPlayerCurrentBullet(int playerID, int newValueToSet)
    {
        RequestLoadDataIfNotSet();
        allPlayersData.playersDataArray[playerID].currentBulletLoaded = newValueToSet;
        DataStorer.SaveData(allPlayersData);
    }
}