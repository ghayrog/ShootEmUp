using Game;
using GameUnits;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerMovementController : MonoBehaviour,
        IGameFixedUpdateListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        [SerializeField]
        private MovementComponent _playerMovement;

        [SerializeField]
        private PlayerInput _playerInput;

        public void OnFixedUpdate()
        {
            if (_playerInput.HorizontalDirection != 0) _playerMovement.Move(_playerInput.HorizontalDirection, 0);
        }
    }
}
