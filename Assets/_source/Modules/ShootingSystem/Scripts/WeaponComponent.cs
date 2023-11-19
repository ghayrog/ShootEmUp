using UnityEngine;

namespace ShootingSystem
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private BulletSpawner _bulletSpawner;

        public void Shoot()
        {
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
