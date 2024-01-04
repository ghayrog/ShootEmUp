using Game;
using DI;

namespace GameUI
{
    internal sealed class ResumeButtonListener : 
        IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        private GameManager _gameManager;

        private ButtonCollection _resumeButtonCollection;

        public float ExecutionPriority => (float)LoadingPriority.Low;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
            _resumeButtonCollection = new ButtonCollection(ButtonTypes.Resume, gameManager.gameObject.scene);
            _resumeButtonCollection.DeactivateAll();
        }

        public void OnGameStart()
        {
            _resumeButtonCollection.AddAllListeners(ResumeGame);
            _resumeButtonCollection.DeactivateAll();
        }

        public void OnGamePause()
        {
            _resumeButtonCollection.ActivateAll();
        }

        public void OnGameResume()
        {
            _resumeButtonCollection.DeactivateAll();
        }

        public void OnGameFinish()
        {
            _resumeButtonCollection.RemoveAllListeners(ResumeGame);
            _resumeButtonCollection.DeactivateAll();
        }

        private void ResumeGame()
        {
            _gameManager.ResumeGame();
        }
    }
}
