using System;
using UnityEngine;
using Game;

namespace Player
{
    internal sealed class PlayerInput : 
        IGameUpdateListener
    {
        public event Action OnFire;

        public float ExecutionPriority => (float)LoadingPriority.Low;

        internal float HorizontalDirection { get; private set; }

        private const string HORIZONTAL = "Horizontal";

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFire?.Invoke();
            }

            HorizontalDirection = Input.GetAxisRaw(HORIZONTAL);
        }
    }
}
