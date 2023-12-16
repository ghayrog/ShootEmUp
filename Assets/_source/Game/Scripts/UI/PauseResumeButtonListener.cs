using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    internal sealed class PauseResumeButtonListener : MonoBehaviour,
        IGameStartListener, IGameFinishListener
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private Button _pauseButton;

        [SerializeField]
        private Button _resumeButton;

        public float ExecutionPriority => (float)LoadingPriority.Low;

        public void OnGameStart()
        {
            _pauseButton.onClick.AddListener(PauseGame);
            _resumeButton.onClick.AddListener(ResumeGame);
        }

        public void OnGameFinish()
        {
            _pauseButton.onClick.RemoveListener(PauseGame);
            _resumeButton.onClick.RemoveListener(ResumeGame);
        }

        private void PauseGame()
        {
            _gameManager.PauseGame();
        }

        private void ResumeGame()
        {
            _gameManager.ResumeGame();
        }
    }
}
