using UnityEngine;
using DI;
using Game;

namespace EnemySystem
{
    internal sealed class EnemyGameModuleInstaller : GameModuleInstaller
    {
        [SerializeField, Listener, Service(typeof(EnemySpawner))]
        private EnemySpawner _enemySpawner;

        [SerializeField, Listener]
        private EnemyPeriodicSpawner _enemyPeriodicSpawner;
    }
}
