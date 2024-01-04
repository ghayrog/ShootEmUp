using UnityEngine;
using DI;
using Utilities;

namespace Game
{
    internal sealed class MainGameModuleInstaller : GameModuleInstaller
    {
        [SerializeField, Service(typeof(Timer))]
        private Timer _timer;

        private GameCooldownLauncher _gameCooldownLauncher = new();
    }
}
