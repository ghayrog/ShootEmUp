using UnityEngine;
using DI;
using Game;

namespace GameUI
{
    internal sealed class UIGameModuleInstaller : GameModuleInstaller
    {
        [SerializeField, Service(typeof(GameText))]
        private GameText _gameText;

        [Listener]
        private StartButtonListener _startButtonListener = new();

        [Listener]
        private PauseButtonListener _pauseButtonListener = new();

        [Listener]
        private ResumeButtonListener _resumeButtonListener = new();

        [SerializeField]
        private UITextTimerController _uiTextTimerController;
    }
}
