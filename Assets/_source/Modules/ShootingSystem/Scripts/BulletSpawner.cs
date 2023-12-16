using UnityEngine;
using Common;
using Game;
using System.Collections.Generic;

namespace ShootingSystem
{ 
    public sealed class BulletSpawner : MonoBehaviour,
        IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener,
        IGameFixedUpdateListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        [SerializeField]
        private ObjectPool _bulletPool;

        [SerializeField]
        private BulletBoundary _bulletBoundary;

        private List<Bullet> _bullets = new();

        public void OnGameStart()
        {
        }

        public void OnFixedUpdate()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].OnFixedUpdate();
            }
        }

        public void OnGamePause()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].OnGamePause();
            }
        }

        public void OnGameResume()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].OnGameResume();
            }
        }

        public void OnGameFinish()
        {
            _bullets.Clear();
        }

        internal void SpawnBullet(Transform firePoint, BulletConfig bulletConfig)
        {
            var bullet = _bulletPool.GetFromPool().GetComponent<Bullet>();
            bullet.Activate(firePoint.rotation * Vector3.up, firePoint.position, bulletConfig, _bulletBoundary);
            bullet.OnEndOfLife += ReturnBullet;
            _bullets.Add(bullet);
        }

        internal void ReturnBullet(Bullet bullet)
        {
            bullet.OnEndOfLife -= ReturnBullet;
            bullet.Deactivate();
            _bulletPool.ReturnToPool(bullet.gameObject);
            _bullets.Remove(bullet);

        }
    }
}