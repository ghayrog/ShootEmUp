using UnityEngine;

namespace EnemySystem
{
    internal sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPositions;

        [SerializeField]
        private Transform[] attackPositions;

        internal Transform RandomSpawnPosition()
        {
            return this.RandomTransform(this.spawnPositions);
        }

        internal Transform RandomAttackPosition()
        {
            return this.RandomTransform(this.attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}
