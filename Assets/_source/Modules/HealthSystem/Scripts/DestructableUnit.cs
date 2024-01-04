using UnityEngine;

namespace HealthSystem
{
    public sealed class DestructableUnit : MonoBehaviour
    {
        [field: SerializeField]
        public HealthComponent HealthComponent { get; private set; }
    }
}
