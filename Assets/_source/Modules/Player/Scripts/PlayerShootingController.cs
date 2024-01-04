using DI;
using Game;
using GameUnits;
using ShootingSystem;

namespace Player
{
    internal sealed class PlayerShootingController :
        IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        private WeaponComponent _weapon;

        private PlayerInput _playerInput;

        private GameUnit _gameUnit;

        [Inject]
        internal void Construct(GameUnit gameUnit, PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _gameUnit = gameUnit;
        }

        public void OnGameStart()
        {
            _weapon = _gameUnit.WeaponComponent;
            _playerInput.OnFire += Fire;
        }

        public void OnGamePause()
        {
            _playerInput.OnFire -= Fire;
        }

        public void OnGameResume()
        {
            _playerInput.OnFire += Fire;
        }

        public void OnGameFinish()
        {
            _playerInput.OnFire -= Fire;
        }

        private void Fire()
        {
            _weapon.Shoot();
        }

    }
}
