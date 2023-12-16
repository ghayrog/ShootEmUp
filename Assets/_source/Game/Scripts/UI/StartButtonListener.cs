using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Game
{
    internal sealed class StartButtonListener : MonoBehaviour,
        IGameStartListener
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Timer _timer;

        public float ExecutionPriority => (float)LoadingPriority.Low;

        private void Awake()
        {
            _startButton.onClick.AddListener(StartTimer);
        }

        public void OnGameStart()
        {
            _startButton.gameObject.SetActive(false);
        }

        private void StartTimer()
        {
            _timer.StartTimer();

            _startButton.gameObject.SetActive(false);
            _startButton.onClick.RemoveListener(StartTimer);
        }
    }
}
