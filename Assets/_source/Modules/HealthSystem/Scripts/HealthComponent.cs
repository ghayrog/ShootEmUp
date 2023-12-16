using Game;
using System;
using UnityEngine;

namespace HealthSystem
{
    public sealed class HealthComponent : MonoBehaviour,
        IGameStartListener
    {
        public event Action<GameObject> OnDeath;
        public event Action<GameObject> OnTakeDamage;

        [SerializeField]
        private int _maxHitPoints;

        private int _hitPoints;

        public float ExecutionPriority => (float)LoadingPriority.Low;

        public void OnGameStart()
        {
            _hitPoints = _maxHitPoints;
        }

        public void TakeDamage(int damage)
        {
            if (_hitPoints > 0)
            {
                OnTakeDamage?.Invoke(gameObject);
            }

            _hitPoints = Mathf.Max(_hitPoints - damage, 0);

            if (_hitPoints <= 0)
            {
                OnDeath?.Invoke(gameObject);
            }
        }
    }
}
