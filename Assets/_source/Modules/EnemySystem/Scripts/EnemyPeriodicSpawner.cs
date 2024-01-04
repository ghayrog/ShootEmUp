using System.Collections;
using UnityEngine;
using DI;
using Game;

namespace EnemySystem
{
    internal sealed class EnemyPeriodicSpawner : MonoBehaviour,
        IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        private EnemySpawner _enemySpawner;

        [SerializeField]
        private float _spawnInterval = 1f;

        private Coroutine _periodicSpawnCoroutine;
        private bool _isSpawning = false;

        [Inject]
        public void Construct(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public void OnGameStart()
        {
            _periodicSpawnCoroutine = StartCoroutine(PeriodicSpawn());
        }

        public void OnGamePause()
        {
            StopSpawning();
        }

        public void OnGameResume()
        {
            _periodicSpawnCoroutine = StartCoroutine(PeriodicSpawn());
        }

        public void OnGameFinish()
        {
            StopSpawning();
        }

        private IEnumerator PeriodicSpawn()
        {
            _isSpawning = true;
            while (_isSpawning)
            {
                _enemySpawner.TrySpawnEnemy();
                yield return new WaitForSeconds(_spawnInterval);
            }
            _periodicSpawnCoroutine = null;
        }

        internal void StopSpawning()
        {
            if (_periodicSpawnCoroutine != null) StopCoroutine(_periodicSpawnCoroutine);
            _isSpawning = false;
        }
    }
}
