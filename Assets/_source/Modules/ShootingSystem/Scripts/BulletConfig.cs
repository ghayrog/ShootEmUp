using UnityEngine;
using TeamsSystem;

namespace ShootingSystem
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField]
        public Team team;

        [SerializeField]
        public Color color;

        [SerializeField]
        public int damage;

        [SerializeField]
        public float speed;
    }
}