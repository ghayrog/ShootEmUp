using UnityEngine;
using GameUnits;
using ShootingSystem;

namespace EnemySystem
{
    internal sealed class EnemyAI : MonoBehaviour
    {
        private const float MIN_DISTANCE_ERROR = 0.25f;

        [SerializeField] private WeaponComponent _weapon;
        [SerializeField] private StarshipMovement _movement;
        [SerializeField] private float _shootingCooldownTimer;

        private Transform _moveTarget;
        private Transform _aimTarget;
        private float _shootingElapsedTime;

        internal void SetTargets(Transform moveTarget, Transform aimTarget)
        {
            _moveTarget = moveTarget;
            _aimTarget= aimTarget;
            _shootingElapsedTime = _shootingCooldownTimer;
        }

        internal void SetSpawner(BulletSpawner bulletSpawner)
        {
            _weapon.SetBulletSpawner(bulletSpawner);
        }

        private void FixedUpdate()
        {
            if (_shootingElapsedTime > 0)
            {
                _shootingElapsedTime -= Time.fixedDeltaTime;
            }

            Vector2 vector = _moveTarget.position - transform.position;
            if (vector.magnitude >= MIN_DISTANCE_ERROR)
            {
                _movement.Move(vector.x, vector.y);
                return;
            }

            if (_shootingElapsedTime <= 0)
            {
                Vector2 direction = _aimTarget.position - transform.position;
                _weapon.SetFirePointDirection(direction);
                _weapon.Shoot();
                _shootingElapsedTime = _shootingCooldownTimer;
            }
        }
    }
}
