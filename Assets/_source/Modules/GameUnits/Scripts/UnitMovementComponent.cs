using Game;
using UnityEngine;

namespace GameUnits
{
    public sealed class UnitMovementComponent : MonoBehaviour,
        IGameResumeListener, IGamePauseListener, IGameStartListener, IGameFinishListener
    {
        private const float DEFAULT_SPEED = 5f;
        private Rigidbody2D _rigidbody2D;
        public float Priority => (float)LoadingPriority.Low;
        private bool _isMovementAllowed = false;

        [SerializeField]
        private float _speed = DEFAULT_SPEED;

        public void Move(float horizontalAxis, float verticalAxis)
        {
            if (!_isMovementAllowed) return;
            Vector2 velocity = (new Vector2(horizontalAxis,verticalAxis).normalized) * Time.fixedDeltaTime;
            Vector2 nextPosition = _rigidbody2D.position + velocity * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }

        public void OnGameFinish()
        {
            _isMovementAllowed = false;
        }

        public void OnGameStart()
        {
            _isMovementAllowed = true;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Debug.Log($"Initializing Unit Movement Component for {gameObject.name}");
        }

        public void OnGamePause()
        {
            _isMovementAllowed = false;
        }

        public void OnGameResume()
        {
            _isMovementAllowed = true;
        }
    }
}
