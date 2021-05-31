using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic.Inputs
{
    public class PlayersInputs : MonoBehaviour
    {
        [System.Serializable]
        public class PlayerInput
        {
            [SerializeField]
            private PlayerControls playerControl;

            public int MovementInput => playerControl.MovementDirection();

            public int AimInput => playerControl.AimDirection();

            public bool PressedFire => playerControl.PressedFire;
        }

        [SerializeField]
        private List<PlayerInput> playerList;

        static List<PlayerInput> currentPlayersList;

        private void Awake()
        {
            SetPlayers();
        }

        void SetPlayers()
        {
            currentPlayersList = playerList;
        }

        public static bool PressedPause => Input.GetKeyDown(KeyCode.Escape);

        public static int PlayerMovementDirection(int playerID)
        {
            if (!isThisPlayerAvailable(playerID))
            {
                Debug.LogError("Player not available id: " + playerID);
                return 0;
            }

            return currentPlayersList[playerID].MovementInput;
        }

        public static int PlayerAimDirection(int playerID)
        {
            if (!isThisPlayerAvailable(playerID))
            {
                Debug.LogError("Player not available id: " + playerID);
                return 0;
            }

            return currentPlayersList[playerID].AimInput;
        }

        public static bool PlayerPressedFire(int playerID)
        {
            if (!isThisPlayerAvailable(playerID))
            {
                Debug.LogError("Player not available id: " + playerID);
                return false;
            }

            return currentPlayersList[playerID].PressedFire;
        }

        static bool isThisPlayerAvailable(int playerID)
        {
            if(currentPlayersList == null)
            {
                Debug.Log("Missing player inputs!");
                return false;
            }

            if (playerID < 0 || playerID >= currentPlayersList.Count)
                return false;

            return true;
        }
    }
}