using UnityEngine;
using Utilities;

namespace Game
{
    internal sealed class GameCooldownLauncher : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private Timer _timer;

        private void Awake()
        {
            _timer.OnTimerCompleted += StartGame;
        }

        private void OnDestroy()
        {
            _timer.OnTimerCompleted -= StartGame;
        }

        private void StartGame()
        {
            _gameManager.StartGame();
            _timer.OnTimerCompleted -= StartGame;
        }
    }
}
