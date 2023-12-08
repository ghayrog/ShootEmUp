using GameUnits;
using UnityEngine;

namespace EnemySystem
{
    internal sealed class EnemyMovement
    {
        private const float MIN_DISTANCE_ERROR = 0.25f;
        private UnitMovementComponent _movement;
        private Transform _selfTransform;
        private Transform _moveTarget;

        internal bool isTargetReached { get; private set; }

        internal EnemyMovement(UnitMovementComponent movement, Transform selfTransform, Transform moveTarget)
        { 
            _movement = movement;
            _selfTransform = selfTransform;
            _moveTarget = moveTarget;
        }

        internal void SetTarget(Transform moveTarget)
        {
            _moveTarget = moveTarget;
        }

        internal void FixedUpdate()
        {
            Vector2 vector = _moveTarget.position - _selfTransform.position;
            if (vector.magnitude >= MIN_DISTANCE_ERROR)
            {
                //Debug.Log("Moving enemy");
                _movement.Move(vector.x, vector.y);
                isTargetReached = false;
            }
            else
            { 
                isTargetReached = true;
            }
        }
    }
}
