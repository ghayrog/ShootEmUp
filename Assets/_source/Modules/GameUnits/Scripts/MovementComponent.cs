using Game;
using UnityEngine;

namespace GameUnits
{
    public sealed class MovementComponent : MonoBehaviour,
        IGameStartListener
    {
        public float ExecutionPriority => (float)LoadingPriority.Low;

        [SerializeField]
        private float _speed = DEFAULT_SPEED;

        private Rigidbody2D _rigidbody2D;

        private const float DEFAULT_SPEED = 5f;

        public void OnGameStart()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Debug.Log($"Initializing Unit Movement Component for {gameObject.name}");
        }

        public void Move(float horizontalAxis, float verticalAxis)
        {
            Vector2 velocity = (new Vector2(horizontalAxis, verticalAxis).normalized) * Time.fixedDeltaTime;
            Vector2 nextPosition = _rigidbody2D.position + velocity * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}
