using GameUnits;
using ShootingSystem;
using UnityEngine;

namespace Player
{
    internal class PlayerInputObserver : MonoBehaviour
    {
        [SerializeField]
        private UnitMovementComponent _starshipMovement;

        [SerializeField]
        private WeaponComponent _weapon;

        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = new PlayerInput();
        }

        private void Update()
        {
            _playerInput.Update();
        }

        private void FixedUpdate()
        {
            if (_playerInput == null) return;
            if (_playerInput.HorizontalDirection != 0) _starshipMovement.Move(_playerInput.HorizontalDirection, 0);
            if (_playerInput.FireRequired)
            {
                _weapon.Shoot();
                _playerInput.ResetFireStatus();
            }
        }
    }
}
