using System;
using UnityEngine;
using HealthSystem;
using Game;

namespace Player
{
    public sealed class PlayerController : MonoBehaviour,
        IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener
    {
        public event Action OnPlayerDeath;
        public float Priority => (float)LoadingPriority.Low;

        [SerializeField]
        private HealthComponent _playerHealth;

        private void PlayerDeath(GameObject gameObject)
        {
            OnPlayerDeath?.Invoke();
        }

        public void OnGameStart()
        {
            enabled = true;
            _playerHealth.OnDeath += PlayerDeath;
            _playerHealth.ResetHealth();
        }

        public void OnGameFinish()
        {
            enabled = false;
            _playerHealth.OnDeath -= PlayerDeath;
        }

        public void OnGamePause()
        {
            enabled = false;
        }

        public void OnGameResume()
        {
            enabled = true;
        }
    }
}
