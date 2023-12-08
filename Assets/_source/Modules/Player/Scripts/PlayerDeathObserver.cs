using UnityEngine;
using Game;

namespace Player
{
    internal sealed class PlayerDeathObserver : MonoBehaviour,
        IGameStartListener, IGameFinishListener
    {
        public float Priority => (float)LoadingPriority.Low;

        [SerializeField]
        private PlayerController _playerController;

        private GameOverManager gameOverManager;

        public void OnGameFinish()
        {
            _playerController.OnPlayerDeath -= gameOverManager.FinishGame;
        }

        public void OnGameStart()
        {
            gameOverManager = GetComponent<GameOverManager>();
            _playerController.OnPlayerDeath += gameOverManager.FinishGame;
        }
    }
}
