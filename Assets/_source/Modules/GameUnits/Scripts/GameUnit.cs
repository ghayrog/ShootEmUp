using UnityEngine;
using Game;
using HealthSystem;
using ShootingSystem;

namespace GameUnits
{
    public sealed class GameUnit : MonoBehaviour,
        IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener
    {
        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private BulletConfig _bulletConfig;

        [SerializeField]
        private float _speed;

        public WeaponComponent WeaponComponent { get; private set; }

        public MovementComponent MovementComponent { get; private set; }

        [field: SerializeField]
        public DestructableUnit DestructableUnit { get; private set; }

        public float ExecutionPriority => (float)LoadingPriority.Low;

        public void InitializeUnit(BulletSpawner bulletSpawner)
        {
            if (WeaponComponent != null)
            { 
                WeaponComponent.SetBulletSpawner(bulletSpawner);
            }
            else
            {
                WeaponComponent = new WeaponComponent(_firePoint, _bulletConfig, bulletSpawner);

            }
            if (MovementComponent == null)
            {
                var rb = GetComponent<Rigidbody2D>();
                MovementComponent = new MovementComponent(_speed, rb);
            }
        }

        public void OnGameStart()
        {
            DestructableUnit.HealthComponent.OnGameStart();
            WeaponComponent.OnGameStart();
        }

        public void OnGamePause()
        { 
            WeaponComponent.OnGamePause();
        }

        public void OnGameResume()
        {
            WeaponComponent.OnGameResume();
        }

        public void OnGameFinish()
        {
            WeaponComponent.OnGameFinish();
        }
    }
}
