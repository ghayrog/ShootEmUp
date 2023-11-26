using System;
using UnityEngine;
using HealthSystem;
using ShootingSystem;

namespace EnemySystem
{
    public sealed class EnemyController : MonoBehaviour
    {
        public event Action<EnemyController> OnDeath;
        public bool IsAlive { get; private set; }

        [SerializeField]
        private HealthComponent _health;

        [SerializeField]
        private EnemyAI _enemyAI;

        public void Initialize(Vector3 position, Transform moveTarget, Transform aimTarget, BulletSpawner bulletSpawner)
        { 
            transform.position = position;
            _enemyAI.SetTargets(moveTarget, aimTarget);
            _enemyAI.SetSpawner(bulletSpawner);
            _health.ResetHealth();
        }

        private void Awake()
        {
            IsAlive = true;
        }

        private void OnEnable()
        {
            _health.OnDeath += EnemyDeath;
        }

        private void OnDisable()
        {
            _health.OnDeath -= EnemyDeath;
        }

        private void EnemyDeath(GameObject gameObject)
        {
            OnDeath.Invoke(this);
            IsAlive = false;
        }
    }
}
