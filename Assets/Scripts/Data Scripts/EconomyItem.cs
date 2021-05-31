using UnityEngine;

namespace EconomyTools
{
    public class PlayerCoins
    {
        private int _playerID;

        public PlayerCoins(int playerID)
        {
            _playerID = playerID;
        }

        public bool CheckIfHasCoins(int valueToCheck)
        {
            int auxTotal = CoinsAmmount;

            if (auxTotal >= valueToCheck)
                return true;

            return false;
        }

        public bool ChargeCoins(int valueToCharge)
        {
            int auxTotal = CoinsAmmount;

            if (auxTotal >= valueToCharge)
            {
                auxTotal -= valueToCharge;
                SetNewCoinValue(auxTotal);
                return true;
            }

            return false;
        }

        public void AddCoins(int valueToAdd)
        {
            int auxTotal = CoinsAmmount;

            auxTotal += valueToAdd;
            SetNewCoinValue(auxTotal);
        }

        public string ListTotalCoins()
        {
            return CoinsAmmount.ToString("#,##0").Replace(",", ".");
        }

        int CoinsAmmount => PlayersDataManager.PlayerMoneyAmmout(_playerID);

        void SetNewCoinValue(int newCoinValue)
        {
            PlayersDataManager.SetNewPlayerMoneyAmmout(_playerID, newCoinValue);
        }
    }
}