using UnityEngine;

namespace PlayerLogic.Inputs
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Player Controls", fileName = "New Player Controls")]
    public class PlayerControls : ScriptableObject
    {
        [Header("-----Movement Controls-----")]
        [SerializeField]
        private KeyCode upArrow = KeyCode.W;
        [SerializeField]
        private KeyCode downArrow = KeyCode.S;

        [Header("-----Aim Controls-----")]
        [SerializeField]
        private KeyCode aimUpArrow = KeyCode.A;
        [SerializeField]
        private KeyCode aimDownArrow = KeyCode.D;

        [Header("-----Other Controls-----")]
        [SerializeField]
        private KeyCode fireButton = KeyCode.Space;

        bool PressedButton(KeyCode buttonToCheckPress)
        {
            return Input.GetKeyDown(buttonToCheckPress);
        }
        bool IsPressingButton(KeyCode buttonToCheckPress)
        {
            return Input.GetKey(buttonToCheckPress);
        }

        public int MovementDirection()
        {
            if (IsPressingButton(upArrow) && IsPressingButton(downArrow))
                return 0;

            if (IsPressingButton(upArrow))
                return 1;

            if (IsPressingButton(downArrow))
                return -1;

            return 0;
        }

        public int AimDirection()
        {
            if (IsPressingButton(aimUpArrow) && IsPressingButton(aimDownArrow))
                return 0;

            if (IsPressingButton(aimUpArrow))
                return 1;

            if (IsPressingButton(aimDownArrow))
                return -1;

            return 0;
        }

        public bool PressedFire => PressedButton(fireButton);
    }
}