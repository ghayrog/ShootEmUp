using UnityEngine;
using Player;

namespace Game
{
    internal sealed class GameOverObserver : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GetComponent<GameManager>();
        }

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
            gameManager.FinishGame(player);
        }

    }
}
