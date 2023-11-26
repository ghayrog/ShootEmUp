using GameUnits;
using ShootingSystem;
using UnityEngine;

namespace EnemySystem
{
    internal sealed class EnemyWeapon
    {
        private WeaponComponent _weapon;
        private float _shootingCooldownTimer;
        private Transform _aimTarget;
        private float _shootingElapsedTime;
        private Transform _selfTransform;


        internal EnemyWeapon(WeaponComponent weapon, float shootingCooldownTimer, Transform selfTransform, Transform aimTarget)
        {
            _weapon = weapon;
            _selfTransform = selfTransform;
            _aimTarget = aimTarget;
            _shootingCooldownTimer = shootingCooldownTimer;
        }

        internal void SetTarget(Transform aimTarget)
        {
            _aimTarget = aimTarget;
            _shootingElapsedTime = _shootingCooldownTimer;
        }

        internal void FixedUpdate(float fixedDeltaTime, bool isShootingAllowed)
        {
            if (_shootingElapsedTime > 0)
            {
                _shootingElapsedTime -= fixedDeltaTime;
            }

            if (isShootingAllowed && _shootingElapsedTime <= 0)
            {
                Vector2 direction = _aimTarget.position - _selfTransform.position;
                _weapon.SetFirePointDirection(direction);
                _weapon.Shoot();
                _shootingElapsedTime = _shootingCooldownTimer;
            }
        }
    }
}
