using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _initialCount;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _world;

        private readonly Queue<GameObject> m_poolObjects = new();
        private readonly HashSet<GameObject> m_activeObjects = new();
        private bool _isInitialized = false;

        public void Awake()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var newPoolObject = Instantiate(_prefab, _container);
                this.m_poolObjects.Enqueue(newPoolObject);
            }
            _isInitialized = true;
        }

        public GameObject GetFromPool()
        {
            if (!_isInitialized) return null;
            if (this.m_poolObjects.TryDequeue(out var poolObject))
            {
                poolObject.transform.SetParent(_world);
            }
            else
            {
                poolObject = Instantiate(_prefab, _world);
            }

            this.m_activeObjects.Add(poolObject);
            return poolObject;
        }

        public void ReturnToPool(GameObject poolObject)
        {
            if (!_isInitialized) return;
            if (this.m_activeObjects.Remove(poolObject))
            {
                poolObject.transform.SetParent(_container);
                this.m_poolObjects.Enqueue(poolObject);
            }
        }
    }
}
