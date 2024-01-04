using System;
using UnityEngine;

namespace EnemySystem
{
    [Serializable]
    internal sealed class EnemyPositions
    {
        [SerializeField]
        private Transform[] _spawnPositions;

        [SerializeField]
        private Transform[] _attackPositions;

        internal Transform RandomSpawnPosition()
        {
            return RandomTransform(_spawnPositions);
        }

        internal Transform RandomAttackPosition()
        {
            return RandomTransform(_attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = UnityEngine.Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}
