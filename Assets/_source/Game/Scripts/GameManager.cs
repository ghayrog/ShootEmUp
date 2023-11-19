using UnityEngine;
using Player;

namespace Game
{
    internal sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        private void OnEnable()
        {
            _playerController.OnPlayerDeath += FinishGame;
        }

        private void OnDisable()
        {
            _playerController.OnPlayerDeath -= FinishGame;
        }

        private void FinishGame(PlayerController player)
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}
