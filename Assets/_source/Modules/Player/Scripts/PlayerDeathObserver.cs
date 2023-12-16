using UnityEngine;
using Game;
using HealthSystem;

namespace Player
{
    internal sealed class PlayerDeathObserver : MonoBehaviour,
        IGameStartListener, IGameFinishListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        [SerializeField]
        private HealthComponent _playerHealth;

        [SerializeField]
        private GameManager _gameManager;

        public void OnGameStart()
        {
            _playerHealth.OnDeath += FinishGame;
        }

        public void OnGameFinish()
        {
            _playerHealth.OnDeath -= FinishGame;
        }

        private void FinishGame(GameObject gameObject)
        { 
            _gameManager.FinishGame();
        }
    }
}
