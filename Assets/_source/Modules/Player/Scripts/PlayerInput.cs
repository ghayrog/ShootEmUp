using Game;
using System;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerInput : MonoBehaviour,
        IGameUpdateListener
    {
        public event Action OnFire;

        public float ExecutionPriority => (float)LoadingPriority.Low;

        internal float HorizontalDirection { get; private set; }

        private const string HORIZONTAL = "Horizontal";

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFire?.Invoke();
            }

            HorizontalDirection = Input.GetAxisRaw(HORIZONTAL);
        }
    }
}
