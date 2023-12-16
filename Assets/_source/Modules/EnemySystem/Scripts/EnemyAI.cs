using UnityEngine;
using GameUnits;
using ShootingSystem;
using Game;
using System.Data.Common;

namespace EnemySystem
{
    internal sealed class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private WeaponComponent _weapon;

        [SerializeField]
        private MovementComponent _movement;

        [SerializeField]
        private float _shootingCooldownTimer;

        private EnemyMovement _enemyMovement;
        private EnemyWeapon _enemyWeapon;

        internal void SetTargets(Transform moveTarget, Transform aimTarget)
        {
            Debug.Log($"Setting enemy targets moveTarget: {moveTarget.gameObject.name} aimTarget: {aimTarget.gameObject.name}");
            _enemyMovement.SetTarget(moveTarget);
            _enemyWeapon.SetTarget(aimTarget);
        }

        internal void SetSpawner(BulletSpawner bulletSpawner)
        {
            _weapon.SetBulletSpawner(bulletSpawner);
        }

        internal void OnFixedUpdate()
        {
            _enemyMovement.FixedUpdate();
            _enemyWeapon.FixedUpdate(Time.fixedDeltaTime, _enemyMovement.isTargetReached);
        }

        internal void Initialize()
        {
            Debug.Log("Initializing Enemy AI");
            enabled = true;
            _movement.OnGameStart();
            _weapon.OnGameStart();
            _enemyMovement = new EnemyMovement(_movement, transform, transform);
            _enemyWeapon = new EnemyWeapon(_weapon, _shootingCooldownTimer, transform, transform);
        }

        internal void Deactivate()
        {
            _weapon.OnGameFinish();
            _enemyMovement = null;
            _enemyWeapon = null;
            enabled = false;
        }

        internal void OnGamePause()
        {
            _weapon.OnGamePause();
            enabled = false;
        }

        internal void OnGameResume()
        {
            _weapon.OnGameResume();
            enabled = true;
        }
    }
}
