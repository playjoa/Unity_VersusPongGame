namespace PlayerDataSystem 
{
    [System.Serializable]
    public class PlayersData 
    {
        public int currentLevelToPlay = 0;
        public PlayerData[] playersDataArray;
    }

    [System.Serializable]
    public class PlayerData 
    {
        public int playerID;

        public int currentBulletLoaded = 0;
        public int moneyAmmount = 0;
        public int victoryCount = 0;

        public bool[] hasBulletItem = new bool[] {true, false, false, false, false };
    }
}