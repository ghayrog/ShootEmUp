using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game;

namespace Common
{
    public sealed class ObjectPool : MonoBehaviour,
        IGameStartListener, IGameFinishListener
    {
        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private int _initialCount;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private Transform _world;

        public float ExecutionPriority => (float)LoadingPriority.High;

        private readonly Queue<GameObject> _poolObjects = new();
        private readonly HashSet<GameObject> _activeObjects = new();
        private bool _isInitialized = false;


        public GameObject GetFromPool()
        {
            if (!_isInitialized) return null;
            if (_poolObjects.TryDequeue(out var poolObject))
            {
                poolObject.transform.SetParent(_world);
            }
            else
            {
                poolObject = Instantiate(_prefab, _world);
            }

            _activeObjects.Add(poolObject);
            return poolObject;
        }

        public void ReturnToPool(GameObject poolObject)
        {
            if (!_isInitialized) return;
            if (_activeObjects.Remove(poolObject))
            {
                poolObject.transform.SetParent(_container);
                _poolObjects.Enqueue(poolObject);
            }
        }

        public void OnGameStart()
        {
            enabled = true;
            for (var i = 0; i < _initialCount; i++)
            {
                var newPoolObject = Instantiate(_prefab, _container);
                _poolObjects.Enqueue(newPoolObject);
            }
            _isInitialized = true;
            Debug.Log("Object Pool Initialized");
        }

        public void OnGameFinish()
        {
            enabled = false;
            while (_activeObjects.Count>0)
            {
                Debug.Log($"Cleaning object pool {gameObject.name}: {_activeObjects.Count}");
                ReturnToPool(_activeObjects.ElementAt(0));
            }
            _isInitialized = false;
            while (_poolObjects.TryDequeue(out var poolObject))
            {
                Destroy(poolObject);
            }
        }
    }
}
