using UnityEngine;
using Common;

namespace ShootingSystem
{ 
    public sealed class BulletSpawner : MonoBehaviour
    {
        [SerializeField]
        private ObjectPool _bulletPool;

        [SerializeField]
        private BulletBoundary _bulletBoundary;

        internal void SpawnBullet(Transform firePoint, BulletConfig bulletConfig)
        { 
            var bullet = _bulletPool.GetFromPool().GetComponent<Bullet>();
            bullet.Initialize(firePoint.rotation * Vector3.up, firePoint.position, bulletConfig, _bulletBoundary);
            bullet.OnEndOfLife += ReturnBullet;
        }

        internal void ReturnBullet(Bullet bullet)
        {
            bullet.OnEndOfLife -= ReturnBullet;
            _bulletPool.ReturnToPool(bullet.gameObject);
        }

    }
}