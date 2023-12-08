using Game;
using GameUnits;
using ShootingSystem;
using UnityEngine;

namespace Player
{
    internal class PlayerInputObserver : MonoBehaviour,
        IGameStartListener, IGameFinishListener,
        IGameFixedUpdateListener, IGameUpdateListener
    {
        public float Priority => (float)LoadingPriority.Low;

        [SerializeField]
        private UnitMovementComponent _starshipMovement;

        [SerializeField]
        private WeaponComponent _weapon;

        private PlayerInput _playerInput;
        private bool _isUpdateAllowed = false;

        public void OnUpdate()
        {
            if (!_isUpdateAllowed) return;
            _playerInput.Update();
        }

        public void OnFixedUpdate()
        {
            if (!_isUpdateAllowed) return;
            if (_playerInput == null) return;
            if (_playerInput.HorizontalDirection != 0) _starshipMovement.Move(_playerInput.HorizontalDirection, 0);
            if (_playerInput.FireRequired)
            {
                _weapon.Shoot();
                _playerInput.ResetFireStatus();
            }
        }

        public void OnGameStart()
        {
            _playerInput = new PlayerInput();
            _isUpdateAllowed = true;
        }

        public void OnGameFinish()
        {
            _isUpdateAllowed = false;
        }
    }
}
