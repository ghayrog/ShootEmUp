using System;
using UnityEngine;
using HealthSystem;

namespace Player
{
    public sealed class PlayerController : MonoBehaviour
    {
        public event Action<PlayerController> OnPlayerDeath;
        [SerializeField] private Health _playerHealth;

        private void Awake()
        {
            _playerHealth.ResetHealth();
        }

        private void OnEnable()
        {
            _playerHealth.OnDeath += PlayerDeath;
        }

        private void OnDisable()
        {
            _playerHealth.OnDeath -= PlayerDeath;
        }

        private void PlayerDeath(GameObject gameObject)
        {
            OnPlayerDeath?.Invoke(this);
        }
    }
}
