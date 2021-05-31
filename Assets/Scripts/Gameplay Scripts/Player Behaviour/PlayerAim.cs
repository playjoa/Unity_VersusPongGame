using UnityEngine;
using PlayerLogic.Inputs;

namespace PlayerLogic.Actions
{
    public class PlayerAim : PlayerComponent
    {
        [SerializeField]
        private float aimSensitivity = 150;

        [SerializeField]
        private Transform aimTransform;

        [SerializeField]
        private float minAimAngle = -175, maxAimAngle = 5;

        private float verticalRotation;

        private void Start()
        {
            SetAimToMiddleAngle();
        }

        private void Update()
        {
            if (!GameManager.isPlayersTurn(playerStats))
            {
                ToggleAim(false);
                return;
            }

            ProcessAim();
        }
        void SetAimToMiddleAngle()
        {
            verticalRotation = (minAimAngle + maxAimAngle) / 2;
        }

        void ProcessAim()
        {
            ToggleAim(true);

            verticalRotation += RotationDirectionValue();
            verticalRotation = RotationClamped(verticalRotation);

            aimTransform.localRotation = Quaternion.Euler(0f, 0f, verticalRotation);
        }

        float RotationDirectionValue()
        {
            return PlayersInputs.PlayerAimDirection(playerID) * aimSensitivity * Time.deltaTime;
        }

        float RotationClamped(float refRotation)
        {
            return Mathf.Clamp(refRotation, minAimAngle, maxAimAngle);
        }

        void ToggleAim(bool boolValueToSet)
        {
            if (boolValueToSet && aimTransform.gameObject.activeSelf)
                return;

            if (!boolValueToSet && !aimTransform.gameObject.activeSelf)
                return;

            aimTransform.gameObject.SetActive(boolValueToSet);
        }
    }
}