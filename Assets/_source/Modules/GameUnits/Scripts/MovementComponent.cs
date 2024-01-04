using UnityEngine;

namespace GameUnits
{
    public sealed class MovementComponent
    {
        private float _speed;

        private Rigidbody2D _rigidbody2D;

        public MovementComponent(float speed, Rigidbody2D rigidbody2D)
        {
            _speed = speed;
            _rigidbody2D = rigidbody2D;
        }

        public void Move(float horizontalAxis, float verticalAxis)
        {
            Vector2 velocity = (new Vector2(horizontalAxis, verticalAxis).normalized) * Time.fixedDeltaTime;
            Vector2 nextPosition = _rigidbody2D.position + velocity * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}
