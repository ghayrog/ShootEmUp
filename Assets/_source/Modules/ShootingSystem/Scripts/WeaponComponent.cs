using UnityEngine;

namespace ShootingSystem
{
    public sealed class WeaponComponent
    {
        private Transform _firePoint;

        private BulletConfig _bulletConfig;

        private BulletSpawner _bulletSpawner;

        private bool enabled = false;

        public WeaponComponent(Transform firePoint, BulletConfig bulletConfig, BulletSpawner bulletSpawner)
        {
            _firePoint = firePoint;
            _bulletConfig = bulletConfig;
            _bulletSpawner = bulletSpawner;
        }

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
