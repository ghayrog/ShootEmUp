using System;
using UnityEngine;
using GameUnits;

namespace EnemySystem
{
    [Serializable]
    internal sealed class EnemyAI
    {
        private GameUnit _gameUnit;

        [SerializeField]
        private float _shootingCooldownTimer;

        private EnemyMovement _enemyMovement;
        private EnemyWeapon _enemyWeapon;

        internal void Initialize(GameUnit gameUnit)
        {
            _gameUnit = gameUnit;
            //Debug.Log("Initializing Enemy AI");
            if (_enemyMovement == null) _enemyMovement = new EnemyMovement(gameUnit.MovementComponent, gameUnit.transform, gameUnit.transform);
            if (_enemyWeapon == null) _enemyWeapon = new EnemyWeapon(gameUnit.WeaponComponent, _shootingCooldownTimer, gameUnit.transform, gameUnit.transform);
        }

        internal void SetTargets(Transform moveTarget, Transform aimTarget)
        {
            //Debug.Log($"Setting enemy targets moveTarget: {moveTarget.gameObject.name} aimTarget: {aimTarget.gameObject.name}");
            _enemyMovement.SetTarget(moveTarget);
            _enemyWeapon.SetTarget(aimTarget);
        }

        internal void OnFixedUpdate(float fixedDeltaTime)
        {
            _enemyMovement.FixedUpdate();
            _enemyWeapon.FixedUpdate(Time.fixedDeltaTime, _enemyMovement.isTargetReached);
        }
    }
}
