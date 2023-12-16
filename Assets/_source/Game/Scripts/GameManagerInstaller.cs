using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(GameManager))]
    internal sealed class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField]
        private Transform _worldObjectContainer;

        private void Awake()
        {
            var manager = GetComponent<GameManager>();
            
            var systemListeners = GetComponentsInChildren<IGameListener>();
            manager.AddMultipleListeners(systemListeners);

            var worldListeners = _worldObjectContainer.GetComponentsInChildren<IGameListener>();
            manager.AddMultipleListeners(worldListeners);
        }
    }
}
