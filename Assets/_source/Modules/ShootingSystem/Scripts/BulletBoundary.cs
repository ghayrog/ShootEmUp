using System;
using UnityEngine;

namespace ShootingSystem
{
    [Serializable]
    internal sealed class BulletBoundary
    {
        [SerializeField]
        private Transform _leftBorder;

        [SerializeField]
        private Transform _rightBorder;

        [SerializeField]
        private Transform _downBorder;

        [SerializeField]
        private Transform _topBorder;

        internal bool InBoundaries(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > _leftBorder.position.x
                   && positionX < _rightBorder.position.x
                   && positionY > _downBorder.position.y
                   && positionY < _topBorder.position.y;
        }
    }
}
