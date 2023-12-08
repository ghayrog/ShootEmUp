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
        private UnitMovementComponent _movement;

        [SerializeField]
        private float _shootingCooldownTimer;

        private EnemyMovement _enemyMovement;
        private EnemyWeapon _enemyWeapon;
        private bool _isUpdateAllowed = false;

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
            if (!_isUpdateAllowed) return;
            _enemyMovement.FixedUpdate();
            _enemyWeapon.FixedUpdate(Time.fixedDeltaTime, _enemyMovement.isTargetReached);
        }

        internal void Initialize()
        {
            Debug.Log("Initializing Enemy AI");
            enabled = true;
            _isUpdateAllowed= true;
            _movement.OnGameStart();
            _weapon.OnGameStart();
            _enemyMovement = new EnemyMovement(_movement, transform, transform);
            _enemyWeapon = new EnemyWeapon(_weapon, _shootingCooldownTimer, transform, transform);
        }

        internal void Deactivate()
        {
            _movement.OnGameFinish();
            _weapon.OnGameFinish();
            _enemyMovement = null;
            _enemyWeapon = null;
            _isUpdateAllowed = false;
            enabled = false;
        }

        internal void OnGamePause()
        {
            _movement.OnGamePause();
            _weapon.OnGamePause();
            enabled = false;
        }

        internal void OnGameResume()
        {
            _movement.OnGameResume();
            _weapon.OnGameResume();
            enabled = true;
        }
    }
}
