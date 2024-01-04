using DI;
using Utilities;

namespace Game
{
    internal sealed class GameCooldownLauncher
    {
        private Timer _timer;
        private GameManager _gameManager;

        [Inject]
        internal void Construct(GameManager gameManager, Timer timer)
        {
            _gameManager = gameManager;
            _timer = timer;
            _timer.OnTimerCompleted += StartGame;
        }

        private void StartGame()
        {
            //Debug.Log("Start Game from Timer");
            _gameManager.StartGame();
            _timer.OnTimerCompleted -= StartGame;
        }
    }
}
