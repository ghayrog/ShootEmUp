using UnityEngine;

namespace GameUnits
{
    public sealed class StarshipMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed = 5.0f;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(float horizontalAxis, float verticalAxis)
        {
            Vector2 velocity = (new Vector2(horizontalAxis,verticalAxis).normalized) * Time.fixedDeltaTime;
            Vector2 nextPosition = _rigidbody2D.position + velocity * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}
