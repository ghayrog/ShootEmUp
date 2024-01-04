using DI;
using Game;

namespace GameUI
{
    internal sealed class PauseButtonListener :
        IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        private GameManager _gameManager;

        private ButtonCollection _pauseButtonCollection;

        public float ExecutionPriority => (float)LoadingPriority.Low;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
            _pauseButtonCollection = new ButtonCollection(ButtonTypes.Pause, gameManager.gameObject.scene);
            _pauseButtonCollection.DeactivateAll();
        }

        public void OnGameStart()
        {
            _pauseButtonCollection.AddAllListeners(PauseGame);
            _pauseButtonCollection.ActivateAll();
        }

        public void OnGamePause()
        {
            _pauseButtonCollection.DeactivateAll();
        }

        public void OnGameResume()
        {
            _pauseButtonCollection.ActivateAll();
        }

        public void OnGameFinish()
        {
            _pauseButtonCollection.RemoveAllListeners(PauseGame);
            _pauseButtonCollection.DeactivateAll();
        }

        private void PauseGame()
        {
            _gameManager.PauseGame();
        }
    }
}
