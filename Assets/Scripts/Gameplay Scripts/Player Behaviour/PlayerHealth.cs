using System;
using UnityEngine;

namespace PlayerLogic.Actions
{
    public class PlayerHealth : PlayerComponent
    {
        [SerializeField]
        private CameraShakeData cameraDamageTakenShake, cameraDeathShake;

        [SerializeField]
        private string playerExploID = "playerExplo";

        private int totalHealth;
        public static Action<PlayerStats> OnPlayerDied;
        public static Action<CameraShakeData> RequestCameraShake;

        public int PlayerID => playerID;
        public int CurrentPlayerHealth { get; private set; }
        public float HealthPorcentage => (float)CurrentPlayerHealth / totalHealth;
        void UpdatePlayerHealthInUI() => UI_ManagerGameplay.Instance.UpdatePlayerHealthUI(this);

        private void Start()
        {
            GetHealthValues();
        }

        public override void OnEnable()
        {
            base.OnEnable();

            HealthBonusAward.HealPlayer += GainHealth;
        }

        private void OnDisable()
        {
            HealthBonusAward.HealPlayer -= GainHealth;
        }

        void GetHealthValues()
        {
            totalHealth = playerStats.PlayerHealth;
            CurrentPlayerHealth = totalHealth;
            UpdatePlayerHealthInUI();
        }

        public void TakeDamage(int damageTaken)
        {
            if (!GameState.isPlaying())
                return;

            CurrentPlayerHealth = ClampHealth(CurrentPlayerHealth - damageTaken);

            if (CurrentPlayerHealth <= 0)
                PlayerDied();
            else
                RequestCameraShake?.Invoke(cameraDamageTakenShake);

            UpdatePlayerHealthInUI();
        }

        public void GainHealth(int playerIDToHeal, int healthGained)
        {
            if (playerIDToHeal != playerID)
                return;

            CurrentPlayerHealth = ClampHealth(CurrentPlayerHealth + healthGained);
            UpdatePlayerHealthInUI();
        }

        int ClampHealth(int valueToClamp) 
        {
            return Mathf.Clamp(valueToClamp, 0, totalHealth);
        }

        void ActivateDeathFeedback() 
        {
            ObjectPooler.Instance.RequestObject(playerExploID, transform.position);
            RequestCameraShake?.Invoke(cameraDeathShake);
        }

        void PlayerDied()
        {
            ActivateDeathFeedback();

            playerStats.KillPlayer();
            OnPlayerDied?.Invoke(playerStats);
        }
    }
}