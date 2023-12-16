using UnityEngine;
using Game;

namespace ShootingSystem
{
    public sealed class WeaponComponent : MonoBehaviour,
        IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private BulletConfig _bulletConfig;

        [SerializeField]
        private BulletSpawner _bulletSpawner;

        public void OnGameStart()
        {
            enabled = true;
        }

        public void OnGamePause()
        {
            enabled = false;
        }

        public void OnGameResume()
        {
            enabled = true;
        }

        public void OnGameFinish()
        {
            enabled = false;
        }

        public void Shoot()
        {
            if (!enabled) return;
            _bulletSpawner.SpawnBullet(_firePoint, _bulletConfig);
        }

        public void SetFirePointDirection(Vector2 direction)
        {
            _firePoint.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

        public void SetBulletSpawner(BulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
        }
    }
}
