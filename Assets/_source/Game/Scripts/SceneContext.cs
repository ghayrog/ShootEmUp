using UnityEngine;
using DI;

namespace Game
{
    [RequireComponent(typeof(GameManager))]
    internal sealed class SceneContext : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour[] _modules;

        private ServiceLocator _serviceLocator;

        private GameManager _gameManager;

        private void Awake()
        {
            _serviceLocator = new ServiceLocator();
            _gameManager = GetComponent<GameManager>();
            _serviceLocator.BindService(typeof(GameManager), _gameManager);

            InstallServices();
            Debug.Log("Game services installed");
            InjectAllDependencies();
            Debug.Log("Game dependencies injected");
            AddListenersToGameManager();
            Debug.Log("Game listeners activated");
        }

        private void InstallServices()
        {
            foreach (var module in _modules)
            {
                if (module is IServiceProvider serviceProvider)
                {
                    var services = serviceProvider.ProvideServices();
                    foreach (var (type, service) in services)
                    {
                        _serviceLocator.BindService(type, service);
                    }
                }
            }
        }

        private void AddListenersToGameManager()
        {
            foreach (var module in _modules)
            {
                if (module is IGameListenerProvider listenersProvider)
                {
                    Debug.Log($"Adding multiple listeners from module {module} as a custom Listener Provider");
                    _gameManager.AddMultipleListeners(listenersProvider.ProvideListeners());
                }
                if (module is IGameListener tListener)
                {
                    Debug.Log($"Adding single listener from module {module}");
                    _gameManager.AddListener(tListener);
                }
            }
        }

        private void InjectAllDependencies()
        {
            foreach (var module in _modules)
            {
                if (module is IInjectProvider injectProvider)
                {
                    Debug.Log($"Inject to {injectProvider}");
                    injectProvider.Inject(_serviceLocator);
                }
            }
            
            GameObject[] gameObjects = gameObject.scene.GetRootGameObjects();

            foreach (var gameObj in gameObjects)
            {
                Inject(gameObj.transform);
            }            
        }

        private void Inject(Transform targetTransform)
        {
            var targets = targetTransform.GetComponents<MonoBehaviour>();
            foreach (var target in targets)
            {
                DependencyInjector.Inject(target, _serviceLocator);
            }

            foreach (Transform child in targetTransform)
            {
                Inject(child);
            }
        }
    }
}    
