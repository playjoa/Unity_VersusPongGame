using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(PlayerStats))]
    public abstract class PlayerComponent : MonoBehaviour
    {
        protected int playerID;
        protected PlayerStats playerStats;

        public virtual void OnEnable()
        {
            GetPlayerValues();
        }

        private void GetPlayerValues()
        {
            playerStats = GetComponent<PlayerStats>();

            playerID = playerStats.PlayerID;
        }
    }
}
