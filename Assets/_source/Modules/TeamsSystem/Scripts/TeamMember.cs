using UnityEngine;

namespace TeamsSystem
{
    public sealed class TeamMember : MonoBehaviour
    {
        [SerializeField] private Team _team;

        public Team Team
        {
            get { return _team; }
        }
    }
}
