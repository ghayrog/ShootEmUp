using UnityEngine;

namespace ShootingSystem
{
    internal sealed class BulletBoundary : MonoBehaviour
    {

        [SerializeField]
        private Transform leftBorder;

        [SerializeField]
        private Transform rightBorder;

        [SerializeField]
        private Transform downBorder;

        [SerializeField]
        private Transform topBorder;

        internal bool InBoundaries(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > this.leftBorder.position.x
                   && positionX < this.rightBorder.position.x
                   && positionY > this.downBorder.position.y
                   && positionY < this.topBorder.position.y;
        }
    }
}
