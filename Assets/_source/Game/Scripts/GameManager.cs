using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;

namespace Game
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameState _gameState;

        private readonly List<IGameStartListener> gameStartListeners = new();
        private readonly List<IGameFinishListener> gameFinishListeners = new();
        private readonly List<IGamePauseListener> gamePauseListeners = new();
        private readonly List<IGameResumeListener> gameResumeListeners = new();

        private readonly List<IGameUpdateListener> gameUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> gameLateUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> gameFixedUpdateListeners = new();

        private void FixedUpdate()
        {
            if (_gameState != GameState.Playing) return;
            for (var i = 0; i < gameFixedUpdateListeners.Count; i++)
            {
                gameFixedUpdateListeners[i].OnFixedUpdate();
            }
        }

        private void Update()
        {
            if (_gameState != GameState.Playing) return;
            for (var i = 0; i < gameUpdateListeners.Count; i++)
            {
                gameUpdateListeners[i].OnUpdate();
            }
        }

        private void LateUpdate()
        {
            if (_gameState != GameState.Playing) return;
            for (var i = 0; i < gameLateUpdateListeners.Count; i++)
            {
                gameLateUpdateListeners[i].OnLateUpdate();
            }
        }

        internal void AddMultipleListeners(IGameListener[] listeners)
        {
            for (int i = 0; i < listeners.Length; i++)
            {
                AddListener(listeners[i]);
            }
        }

        internal void AddListener(IGameListener listener)
        {
            if (listener is IGameStartListener startListener)
            {
                gameStartListeners.Add(startListener);
                gameStartListeners.Sort(
                    (IGameStartListener listener1, IGameStartListener listener2) =>
                    {
                        return (int)Mathf.Sign(listener1.ExecutionPriority - listener2.ExecutionPriority);
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
        public void StartGame()
        {
            if (_gameState != GameState.None && _gameState != GameState.Finished) return;
            for (var i = 0; i < gameStartListeners.Count; i++)
            {
                gameStartListeners[i].OnGameStart();
            }
            _gameState = GameState.Playing;
        }

        [ProPlayButton]
        public void PauseGame()
        {
            if (_gameState != GameState.Playing) return;
            for (var i = 0; i < gamePauseListeners.Count; i++)
            {
                gamePauseListeners[i].OnGamePause();
            }
            _gameState = GameState.Paused;
        }

        [ProPlayButton]
        public void ResumeGame()
        {
            if (_gameState != GameState.Paused) return;
            for (var i = 0; i < gameResumeListeners.Count; i++)
            {
                gameResumeListeners[i].OnGameResume();
            }
            _gameState = GameState.Playing;
        }

        [ProPlayButton]
        public void FinishGame()
        {
            if (_gameState == GameState.None || _gameState == GameState.Finished) return;
            for (var i = 0; i < gameFinishListeners.Count; i++)
            {
                gameFinishListeners[i].OnGameFinish();
            }
            _gameState = GameState.Finished;
            Debug.Log("Game over!");
        }
    }
}
