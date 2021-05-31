using UnityEngine;
using PlayerLogic.Inputs;

namespace PlayerLogic.Actions
{
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerMovement : PlayerComponent
    {
        [SerializeField]
        private float playerVelocity = 5;

        void ControlPlayer()
        {
            float moveDirection = PlayersInputs.PlayerMovementDirection(playerID) * playerVelocity * Time.deltaTime;

            if (isMovingOutDown(moveDirection))
                moveDirection = 0;

            if (isMovingOutUp(moveDirection))
                moveDirection = 0;

            transform.Translate(moveDirection * Vector2.up);
        }

        bool isMovingOutUp(float moveDir)
        {
            if (transform.position.y > GameBoundaries.TopBoundarie.y - playerStats.PlayerHeight / 2 && moveDir > 0)
                return true;

            return false;
        }

        bool isMovingOutDown(float moveDir)
        {
            if (transform.position.y < GameBoundaries.BottomBoundarie.y + playerStats.PlayerHeight / 2 && moveDir < 0)
                return true;

            return false;
        }

        private void Update()
        {
            if (!GameState.isPlaying())
                return;

            ControlPlayer();
        }
    }
}