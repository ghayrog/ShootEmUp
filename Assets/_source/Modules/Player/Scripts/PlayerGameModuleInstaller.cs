using UnityEngine;
using DI;
using Game;
using GameUnits;
using ShootingSystem;

namespace Player
{
    internal sealed class PlayerGameModuleInstaller : GameModuleInstaller
    {
        [Service(typeof(PlayerInput)), Listener]
        private PlayerInput _playerInput = new();

        [SerializeField, Service(typeof(GameUnit)), Listener]
        private GameUnit _gameUnit;

        /*
        [SerializeField, Service(typeof(DestructableUnit))]
        private DestructableUnit _playerDestructableUnit;

        [SerializeField, Service(typeof(MovementComponent)), Listener]
        private MovementComponent _playerMovement;

        [SerializeField, Service(typeof(WeaponComponent)), Listener]
        private WeaponComponent _playerWeapon;
        */

        [Inject]
        public void Construct(BulletSpawner bulletSpawner)
        {
            Debug.Log("Construct Player Game Module Installer");
            _gameUnit.InitializeUnit(bulletSpawner);
        }

        [Listener]
        private PlayerMovementController _playerMovementController = new();

        [Listener]
        private PlayerShootingController _playerShootingController = new();

        [Listener]
        private PlayerDeathObserver _playerDeathObserver = new();

    }
}
