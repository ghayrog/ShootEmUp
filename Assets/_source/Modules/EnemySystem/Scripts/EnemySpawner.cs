using System.Collections;
using UnityEngine;
using Common;
using ShootingSystem;

namespace EnemySystem
{
    internal sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int _maxEnemyCount = 7;
        [SerializeField] private ObjectPool _enemyPool;
        [SerializeField] private float _spawnInterval = 1f;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private BulletSpawner _bulletSpawner;

        private int _enemyCount = 0;
        private Coroutine _periodicSpawnCoroutine;
        private bool _isSpawning = false;

        private void OnEnable()
        {
            _enemyCount = 0;
        }

        private IEnumerator PeriodicSpawn()
        { 
            while (_isSpawning)
            {
                TrySpawnEnemy();
                yield return new WaitForSeconds(_spawnInterval);
            }
            _periodicSpawnCoroutine = null;
        }

        private void Start()
        {
            _isSpawning = true;
            _periodicSpawnCoroutine = StartCoroutine(PeriodicSpawn());
        }

        internal void StopSpawning()
        {            
            if (_periodicSpawnCoroutine != null) StopCoroutine(_periodicSpawnCoroutine);
        }

        private void OnDisable()
        {
            StopSpawning();
        }

        internal void TrySpawnEnemy()
        {
            if (_enemyCount >= _maxEnemyCount) return;
            var enemy = _enemyPool.GetFromPool().GetComponent<EnemyController>();
            Transform spawnPoint = _enemyPositions.RandomSpawnPosition();
            Transform moveTarget = _enemyPositions.RandomAttackPosition();
            enemy.Initialize(spawnPoint.position, moveTarget, aimTarget, _bulletSpawner);
            enemy.OnDeath += ReturnEnemy;
            _enemyCount++;
        }

        internal void ReturnEnemy(EnemyController enemy)
        {
            enemy.OnDeath -= ReturnEnemy;
            _enemyPool.ReturnToPool(enemy.gameObject);
            _enemyCount--;
        }
    }
}
