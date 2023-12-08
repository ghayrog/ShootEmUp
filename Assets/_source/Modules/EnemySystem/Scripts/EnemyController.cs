using System;
using UnityEngine;
using HealthSystem;
using ShootingSystem;
using Game;

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
            Debug.Log("Initializing Enemy Controller");
            transform.position = position;
            _enemyAI.Initialize();
            _enemyAI.SetTargets(moveTarget, aimTarget);
            _enemyAI.SetSpawner(bulletSpawner);
            _health.ResetHealth();
            _health.OnDeath += EnemyDeath;
            IsAlive = true;
        }

        private void EnemyDeath(GameObject gameObject)
        {
            _health.OnDeath -= EnemyDeath;
            OnDeath.Invoke(this);
            IsAlive = false;
        }
    }
}
