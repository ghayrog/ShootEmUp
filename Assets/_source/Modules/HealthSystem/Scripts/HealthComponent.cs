using System;
using UnityEngine;

namespace HealthSystem
{
    [Serializable]
    public sealed class HealthComponent
    {
        public event Action OnDeath;
        public event Action OnTakeDamage;

        [SerializeField]
        private int _maxHitPoints;

        private int _hitPoints;

        public void OnGameStart()
        {
            _hitPoints = _maxHitPoints;
        }

        public void TakeDamage(int damage)
        {
            if (_hitPoints > 0)
            {
                OnTakeDamage?.Invoke();
            }

            _hitPoints = Mathf.Max(_hitPoints - damage, 0);

            if (_hitPoints <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}
