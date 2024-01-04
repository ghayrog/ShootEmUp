using UnityEngine;
using DI;
using Game;

namespace ShootingSystem
{
    internal sealed class ShootingGameModuleInstaller : GameModuleInstaller
    {
        [SerializeField, Service(typeof(BulletSpawner)), Listener]
        private BulletSpawner _bulletSpawner;
    }
}