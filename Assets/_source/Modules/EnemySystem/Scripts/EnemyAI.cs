using UnityEngine;
using GameUnits;
using ShootingSystem;

namespace EnemySystem
{
    internal sealed class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private WeaponComponent _weapon;

        [SerializeField]
        private UnitMovementComponent _movement;

        [SerializeField]
        private float _shootingCooldownTimer;

        private EnemyMovement _enemyMovement;
        private EnemyWeapon _enemyWeapon;

        private void Awake()
        {
            _enemyMovement = new EnemyMovement(_movement, transform, transform);
            _enemyWeapon = new EnemyWeapon(_weapon, _shootingCooldownTimer, transform, transform);
        }

        internal void SetTargets(Transform moveTarget, Transform aimTarget)
        {
            _enemyMovement.SetTarget(moveTarget);
            _enemyWeapon.SetTarget(aimTarget);
        }

        internal void SetSpawner(BulletSpawner bulletSpawner)
        {
            _weapon.SetBulletSpawner(bulletSpawner);
        }

        private void FixedUpdate()
        {
            _enemyMovement.FixedUpdate();
            _enemyWeapon.FixedUpdate(Time.fixedDeltaTime, _enemyMovement.isTargetReached);
        }
    }
}
