using Game;
using System;
using UnityEngine;

namespace HealthSystem
{
    public sealed class HealthComponent : MonoBehaviour
    {
        public event Action<GameObject> OnDeath;
        public event Action<GameObject> OnTakeDamage;

        [SerializeField]
        private int _maxHitPoints;

        private int _hitPoints;

        public void ResetHealth()
        {
            _hitPoints = _maxHitPoints;
        }

        public void TakeDamage(int damage)
        {
            if (_hitPoints > 0)
            {
                this.OnTakeDamage?.Invoke(this.gameObject);
            }

            _hitPoints = Mathf.Max(_hitPoints - damage, 0);

            if (_hitPoints <= 0)
            {
                this.OnDeath?.Invoke(this.gameObject);
            }
        }
    }
}
