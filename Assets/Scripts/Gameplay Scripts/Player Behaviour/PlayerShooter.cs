using System;
using UnityEngine;
using PlayerLogic.Inputs;

namespace PlayerLogic.Actions
{
    public class PlayerShooter : PlayerComponent
    {
        [SerializeField]
        private CameraShakeData shootCameraShake; 

        [SerializeField]
        private Transform aimTransform;

        public static Action<PlayerStats> OnPlayerShot;
        public static Action<CameraShakeData> RequestCameraShake;

        private void Update()
        {
            if (!GameManager.isPlayersTurn(playerStats))
                return;

            ProcessShooting();
        }

        Vector2 AimingDirection()
        {
            return aimTransform.localRotation * Vector2.up;
        }

        void ProcessShooting()
        {
            if (PlayersInputs.PlayerPressedFire(playerID))
            {
                FireBullet();
            }
        }

        BulletStats RequestBulletFromPool()
        {
            return ObjectPooler.Instance.RequestObject(playerStats.BulletIDPlayer(), aimTransform.position).GetComponent<BulletStats>();
        }

        void ShotFeedback()
        {

            RequestCameraShake?.Invoke(shootCameraShake);
            OnPlayerShot?.Invoke(playerStats);
        }

        void FireBullet()
        {
            BulletStats bulletBeingFired = RequestBulletFromPool();
            bulletBeingFired.SetUpBullet(playerID, AimingDirection(), playerStats.PlayerColor);

            ShotFeedback();
        }
    }
}