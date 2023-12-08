using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    internal class GameUIManager : MonoBehaviour
    {
        private const int START_COUNTER = 3;

        internal event Action OnStartUIButtonClick;
        internal event Action OnPauseUIButtonClick;
        internal event Action OnResumeUIButtonClick;

        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _pauseButton;

        [SerializeField]
        private Button _resumeButton;

        [SerializeField]
        private GameText _gameText;

        [SerializeField]
        private float _startCountdownTimestep;

        internal void Initialize()
        {
            _startButton?.onClick.AddListener(OnStartClick);
            _pauseButton?.onClick.AddListener(OnPauseClick);
            _resumeButton?.onClick.AddListener(OnResumeClick);
            _gameText?.Initialize();
            _startButton.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(false);
        }

        private void OnStartClick()
        {
            _startButton.gameObject.SetActive(false);
            StartCoroutine(StartCountdown());
        }

        private void OnPauseClick()
        {
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(true);
            OnPauseUIButtonClick?.Invoke();
        }

        private void OnResumeClick()
        {
            _pauseButton.gameObject.SetActive(true);
            _resumeButton.gameObject.SetActive(false);
            OnResumeUIButtonClick?.Invoke();
        }

        private IEnumerator StartCountdown()
        {
            for (int i = START_COUNTER; i > 0; i--)
            {
                _gameText.ShowGameMessage(i.ToString(), _startCountdownTimestep/2);
                yield return new WaitForSeconds(_startCountdownTimestep);
            }
            _pauseButton.gameObject.SetActive(true);
            OnStartUIButtonClick?.Invoke();
        }
    }
}
