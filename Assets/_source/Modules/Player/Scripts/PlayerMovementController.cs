using DI;
using Game;
using GameUnits;

namespace Player
{
    internal sealed class PlayerMovementController : 
        IGameFixedUpdateListener, IGameStartListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        private MovementComponent _playerMovement;

        private PlayerInput _playerInput;

        private GameUnit _gameUnit;

        [Inject]
        internal void Construct(GameUnit gameUnit, PlayerInput playerInput)
        {
            _gameUnit = gameUnit;
            _playerInput = playerInput;
        }

        public void OnGameStart()
        {
            _playerMovement = _gameUnit.MovementComponent;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (_playerInput.HorizontalDirection != 0) _playerMovement.Move(_playerInput.HorizontalDirection, 0);
        }

    }
}
