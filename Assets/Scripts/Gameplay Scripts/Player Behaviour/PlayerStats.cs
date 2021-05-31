using UnityEngine;

namespace PlayerLogic
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        private int _playerID = 0;

        [SerializeField]
        private int _totalHealth = 100;

        [SerializeField]
        private Transform _graphicsTransform;

        [SerializeField]
        private SpriteRenderer _playerGraphics;

        [SerializeField]
        private Color _playerColor = Color.blue;

        public bool isPlayerDead { get; private set; } = false;
        public int PlayerID => _playerID;
        public int PlayerHealth => _totalHealth;
        public float PlayerHeight => _graphicsTransform.localScale.y;
        public Color PlayerColor => _playerColor;

        private void OnEnable()
        {
            SetPlayerColor();
        }

        void SetPlayerColor()
        {
            _playerGraphics.color = _playerColor;
        }

        public string LoadedPlayerBulletID()
        {
            return "bullet_" + PlayersDataManager.PlayerCurrentBulletLoaded(_playerID);
        }

        public void RevivePlayer()
        {
            isPlayerDead = false;
            _playerGraphics.enabled = true;
        }

        public void KillPlayer()
        {
            isPlayerDead = true;
            _playerGraphics.enabled = false;
        }
    }
}