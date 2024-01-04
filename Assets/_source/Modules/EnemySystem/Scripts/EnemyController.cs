using System;
using UnityEngine;
using GameUnits;
using ShootingSystem;

namespace EnemySystem
{
    public sealed class EnemyController : MonoBehaviour
    {
        public event Action<EnemyController> OnDeath;
        public bool IsAlive { get; private set; }

        [SerializeField]
        private GameUnit _gameUnit;

        [SerializeField]
        private EnemyAI _enemyAI;

        public void Initialize(Vector3 position, Transform moveTarget, Transform aimTarget, BulletSpawner bulletSpawner)
        {
            //Debug.Log("Initializing Enemy Controller");
            transform.position = position;

            _gameUnit.InitializeUnit(bulletSpawner);
            _gameUnit.OnGameStart();
            _gameUnit.DestructableUnit.HealthComponent.OnDeath += EnemyDeath;

            _enemyAI.Initialize(_gameUnit);
            _enemyAI.SetTargets(moveTarget, aimTarget);

            IsAlive = true;
        }

        private void EnemyDeath()
        {
            _gameUnit.DestructableUnit.HealthComponent.OnDeath -= EnemyDeath;
            OnDeath.Invoke(this);
            IsAlive = false;
        }

        internal void OnFixedUpdate(float fixedDeltaTime)
        {
            _enemyAI.OnFixedUpdate(fixedDeltaTime);
        }

        internal void OnGamePause()
        {
            _gameUnit.WeaponComponent.OnGamePause();
        }

        internal void OnGameResume()
        {
            _gameUnit.WeaponComponent.OnGameResume();
        }
    }
}
