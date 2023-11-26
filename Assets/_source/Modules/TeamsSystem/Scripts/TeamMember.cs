using UnityEngine;

namespace TeamsSystem
{
    public sealed class TeamMember : MonoBehaviour
    {
        [field: SerializeField]
        public Team Team { get; private set; }
    }
}
