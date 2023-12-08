using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ShootingSystem;
using Game;

namespace EnemySystem
{
    internal sealed class EnemySpawner : MonoBehaviour,
        IGameStartListener, IGameFinishListener, IGameFixedUpdateListener, IGamePauseListener, IGameResumeListener
    {
        public float Priority => (float)LoadingPriority.Low;

        [SerializeField]
        private int _maxEnemyCount = 7;

        [SerializeField]
        private ObjectPool _enemyPool;

        [SerializeField]
        private float _spawnInterval = 1f;

        [SerializeField]
        private Transform aimTarget;

        [SerializeField]
        private EnemyPositions _enemyPositions;

        [SerializeField]
        private BulletSpawner _bulletSpawner;

        private int _enemyCount = 0;
        private Coroutine _periodicSpawnCoroutine;
        private bool _isSpawning = false;
        private List<EnemyAI> _enemies = new List<EnemyAI>();

        private IEnumerator PeriodicSpawn()
        { 
            while (_isSpawning)
            {
                TrySpawnEnemy();
                yield return new WaitForSeconds(_spawnInterval);
            }
            _periodicSpawnCoroutine = null;
        }

        internal void StopSpawning()
        {            
            if (_periodicSpawnCoroutine != null) StopCoroutine(_periodicSpawnCoroutine);
        }

        internal void TrySpawnEnemy()
        {
            if (!enabled) return;
            if (_enemyCount >= _maxEnemyCount) return;
            Debug.Log("Trying to spawn an enemy");
            var enemyObject = _enemyPool.GetFromPool();
            var enemy = enemyObject.GetComponent<EnemyController>();
            _enemies.Add(enemyObject.GetComponent<EnemyAI>());
            Transform spawnPoint = _enemyPositions.RandomSpawnPosition();
            Transform moveTarget = _enemyPositions.RandomAttackPosition();
            enemy.Initialize(spawnPoint.position, moveTarget, aimTarget, _bulletSpawner);
            enemy.OnDeath += ReturnEnemy;
            _enemyCount++;
        }

        internal void ReturnEnemy(EnemyController enemy)
        {
            if (!enabled) return;
            enemy.OnDeath -= ReturnEnemy;
            _enemyPool.ReturnToPool(enemy.gameObject);
            _enemies.Remove(enemy.gameObject.GetComponent<EnemyAI>());
            _enemyCount--;
        }

        public void OnGameFinish()
        {
            StopSpawning();
            for (int i = 0; i < _enemies.Count; i++)
            {
                ReturnEnemy(_enemies[i].gameObject.GetComponent<EnemyController>());
            }
            _enemies.Clear();
            enabled = false;
        }

        public void OnGameStart()
        {
            enabled = true;
            _isSpawning = true;
            _enemyCount = 0;
            _periodicSpawnCoroutine = StartCoroutine(PeriodicSpawn());
        }

        public void OnGameResume()
        {
            enabled = true;
            _periodicSpawnCoroutine = StartCoroutine(PeriodicSpawn());
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].OnGameResume();
            }
        }

        public void OnGamePause()
        {
            enabled = false;
            StopSpawning();
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].OnGamePause();
            }
        }

        public void OnFixedUpdate()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].OnFixedUpdate();
            }
        }
    }
}
