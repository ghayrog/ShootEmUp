using Game;
using GameUnits;
using ShootingSystem;
using System;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerShootingController : MonoBehaviour,
        IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        [SerializeField]
        private WeaponComponent _weapon;

        [SerializeField]
        private PlayerInput _playerInput;

        public void OnGameStart()
        {
            _playerInput.OnFire += Fire;
        }

        public void OnGamePause()
        {
            _playerInput.OnFire -= Fire;
        }

        public void OnGameResume()
        {
            _playerInput.OnFire += Fire;
        }

        public void OnGameFinish()
        {
            _playerInput.OnFire -= Fire;
        }

        private void Fire()
        {
            _weapon.Shoot();
        }

    }
}
