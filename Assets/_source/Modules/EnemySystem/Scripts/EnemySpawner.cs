using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI;
using Game;
using Utilities;
using ShootingSystem;
using GameUnits;

namespace EnemySystem
{

    internal sealed class EnemySpawner : MonoBehaviour,
        IGameStartListener, IGameFinishListener, IGameFixedUpdateListener, IGamePauseListener, IGameResumeListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        [SerializeField]
        private int _maxEnemyCount = 7;

        [SerializeField]
        private ObjectPool _enemyPool;

        [SerializeField]
        private EnemyPositions _enemyPositions;

        private Transform aimTarget;

        private BulletSpawner _bulletSpawner;

        private List<EnemyController> _enemies = new();

        [Inject]
        public void Construct(BulletSpawner bulletSpawner, GameUnit playerTarget)
        {
            _bulletSpawner = bulletSpawner;
            aimTarget = playerTarget.gameObject.transform;
        }

        internal void TrySpawnEnemy()
        {
            if (!enabled) return;
            if (_enemies.Count >= _maxEnemyCount) return;
            //Debug.Log("Trying to spawn an enemy");
            var enemyObject = _enemyPool.GetFromPool();
            var enemy = enemyObject.GetComponent<EnemyController>();
            _enemies.Add(enemy);
            Transform spawnPoint = _enemyPositions.RandomSpawnPosition();
            Transform moveTarget = _enemyPositions.RandomAttackPosition();
            enemy.Initialize(spawnPoint.position, moveTarget, aimTarget, _bulletSpawner);
            enemy.OnDeath += ReturnEnemy;
        }

        internal void ReturnEnemy(EnemyController enemy)
        {
            if (!enabled) return;
            enemy.OnDeath -= ReturnEnemy;
            _enemyPool.ReturnToPool(enemy.gameObject);
            _enemies.Remove(enemy);
        }

        public void OnGameStart()
        {
            _enemyPool.InitializePool();
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].OnFixedUpdate(fixedDeltaTime);
            }
        }

        public void OnGamePause()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].OnGamePause();
            }
        }

        public void OnGameResume()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].OnGameResume();
            }
        }

        public void OnGameFinish()
        {
            var enemiesCount = _enemies.Count;
            for (int i = 0; i < enemiesCount; i++)
            {
                ReturnEnemy(_enemies[0].gameObject.GetComponent<EnemyController>());
            }
            _enemies.Clear();
            _enemyPool.CleanPool();
        }
    }
}
