using DI;
using Game;
using GameUnits;
using HealthSystem;

namespace Player
{

    internal sealed class PlayerDeathObserver :
        IGameStartListener, IGameFinishListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        private HealthComponent _playerHealth;

        private GameManager _gameManager;

        private GameUnit _gameUnit;

        [Inject]
        internal void Construct(GameManager gameManager, GameUnit gameUnit)
        {
            _gameManager = gameManager;
            _gameUnit = gameUnit;
        }

        public void OnGameStart()
        {
            _playerHealth = _gameUnit.DestructableUnit.HealthComponent;
            _playerHealth.OnDeath += FinishGame;
        }

        public void OnGameFinish()
        {
            _playerHealth.OnDeath -= FinishGame;
        }

        private void FinishGame()
        { 
            _gameManager.FinishGame();
        }
    }
}
