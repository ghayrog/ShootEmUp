using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(GameManager))]
    internal class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField]
        private Transform _worldObjectContainer;
        private void Awake()
        {
            var manager = GetComponent<GameManager>();
            
            var systemListeners = GetComponentsInChildren<IGameListener>();
            foreach (IGameListener listener in systemListeners)
            {
                manager.AddListener(listener);
            }

            var worldListeners = _worldObjectContainer.GetComponentsInChildren<IGameListener>();
            foreach (IGameListener listener in worldListeners)
            {
                manager.AddListener(listener);
            }
        }
    }
}
