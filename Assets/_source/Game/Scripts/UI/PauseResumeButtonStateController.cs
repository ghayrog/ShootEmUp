using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    internal sealed class PauseResumeButtonStateController : MonoBehaviour,
        IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        [SerializeField]
        private Button _pauseButton;

        [SerializeField]
        private Button _resumeButton;


        public float ExecutionPriority => (float)LoadingPriority.Low;

        public void OnGameStart()
        {
            _pauseButton.gameObject.SetActive(true);
            _resumeButton.gameObject.SetActive(false);
        }

        public void OnGamePause()
        {
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(true);
        }

        public void OnGameResume()
        {
            _pauseButton.gameObject.SetActive(true);
            _resumeButton.gameObject.SetActive(false);
        }

        public void OnGameFinish()
        {
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(false);
        }
    }
}
