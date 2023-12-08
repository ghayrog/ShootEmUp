using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;

namespace Game
{
    internal class GameManager : MonoBehaviour
    {
        internal enum GameState
        {
            None,
            Started,
            Finished,
            Paused,
            Resumed
        }

        [SerializeField]
        private GameUIManager _gameUIManager;

        [SerializeField]
        private GameState _gameState;

        private List<IGameStartListener> gameStartListeners = new List<IGameStartListener>();
        private List<IGameFinishListener> gameFinishListeners = new List<IGameFinishListener>();
        private List<IGamePauseListener> gamePauseListeners = new List<IGamePauseListener>();
        private List<IGameResumeListener> gameResumeListeners = new List<IGameResumeListener>();

        private List<IGameUpdateListener> gameUpdateListeners = new List<IGameUpdateListener>();
        private List<IGameLateUpdateListener> gameLateUpdateListeners = new List<IGameLateUpdateListener>();
        private List<IGameFixedUpdateListener> gameFixedUpdateListeners = new List<IGameFixedUpdateListener>();

        internal void AddListener(IGameListener listener)
        {
            if (listener is IGameStartListener startListener)
            {
                gameStartListeners.Add(startListener);
                gameStartListeners.Sort(
                    (IGameStartListener listener1, IGameStartListener listener2) =>
                    {
                        return (int)Mathf.Sign(listener1.Priority - listener2.Priority);
                    }
                    );
            }
            if (listener is IGameFinishListener finishListener)
            {
                gameFinishListeners.Add(finishListener);
            }
            if (listener is IGamePauseListener pauseListener)
            {
                gamePauseListeners.Add(pauseListener);
            }
            if (listener is IGameResumeListener resumeListener)
            {
                gameResumeListeners.Add(resumeListener);
            }

            if (listener is IGameUpdateListener updateListener)
            {
                gameUpdateListeners.Add(updateListener);
            }
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                gameFixedUpdateListeners.Add(fixedUpdateListener);
            }
            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                gameLateUpdateListeners.Add(lateUpdateListener);
            }
        }

        [ProPlayButton]
        private void GameStart()
        {
            if (_gameState != GameState.None && _gameState != GameState.Finished) return;
            for (var i = 0; i < gameStartListeners.Count; i++)
            {
                gameStartListeners[i].OnGameStart();
            }
            _gameState = GameState.Started;
        }

        [ProPlayButton]
        private void GameFinish()
        {
            if (_gameState == GameState.None || _gameState == GameState.Finished) return;
            for (var i = 0; i < gameFinishListeners.Count; i++)
            {
                gameFinishListeners[i].OnGameFinish();
            }
            _gameState = GameState.Finished;
        }

        [ProPlayButton]
        private void GamePause()
        {
            if (_gameState != GameState.Started) return;
            for (var i = 0; i < gamePauseListeners.Count; i++)
            {
                gamePauseListeners[i].OnGamePause();
            }
            _gameState = GameState.Paused;
        }

        [ProPlayButton]
        private void GameResume()
        {
            if (_gameState != GameState.Paused) return;
            for (var i = 0; i < gameResumeListeners.Count; i++)
            {
                gameResumeListeners[i].OnGameResume();
            }
            _gameState = GameState.Started;
        }

        private void OnEnable()
        {
            _gameUIManager.Initialize();
            _gameUIManager.OnStartUIButtonClick += GameStart;
            _gameUIManager.OnPauseUIButtonClick += GamePause;
            _gameUIManager.OnResumeUIButtonClick += GameResume;
        }

        private void OnDisable()
        {
            _gameUIManager.OnStartUIButtonClick -= GameStart;
            _gameUIManager.OnPauseUIButtonClick -= GamePause;
            _gameUIManager.OnResumeUIButtonClick -= GameResume;
        }


        private void Update()
        {
            for (var i = 0; i < gameUpdateListeners.Count; i++)
            {
                gameUpdateListeners[i].OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            for (var i = 0; i < gameFixedUpdateListeners.Count; i++)
            {
                gameFixedUpdateListeners[i].OnFixedUpdate();
            }
        }

        private void LateUpdate()
        {
            for (var i = 0; i < gameLateUpdateListeners.Count; i++)
            {
                gameLateUpdateListeners[i].OnLateUpdate();
            }
        }

    }
}
