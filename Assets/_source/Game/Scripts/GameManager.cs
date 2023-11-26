using UnityEngine;
using Player;

namespace Game
{
    internal sealed class GameManager : MonoBehaviour
    {
        internal void FinishGame(PlayerController player)
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}
