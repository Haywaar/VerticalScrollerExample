using System.Collections.Generic;
using ConfigLoader.Ship;
using ConfigLoader.Ship.JSON;
using ConfigLoader.Ship.ScriptableObject;
using CustomEventBus;
using UI;
using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    public class ServiceLocatorLoader_Menu : MonoBehaviour
    {
        //[SerializeField] private ScriptableObjectLevelLoader _levelLoader;
        [SerializeField] private GUIHolder _guiHolder;
        
        [SerializeField] private ScriptableObjectShipLoader _shipLoaderSO;
        [SerializeField] private ScriptableObjectLevelLoader _levelLoaderSO;
        
        [SerializeField] private ConfigDataLoader _configDataLoader;
        
        private GoldController _goldController;
        private ScoreController _scoreController;

        private List<IDisposable> _disposables = new List<IDisposable>();

        private void Awake()
        {
            ServiceLocator.Initialize();

            var eventBus = new EventBus();
            ServiceLocator.Current.Register(eventBus);
            ServiceLocator.Current.Register<GUIHolder>(_guiHolder);

            InitJSONLoaders();
          //  InitLocalLoaders();

            _goldController = new GoldController();
            ServiceLocator.Current.Register(_goldController);
            _disposables.Add(_goldController);

            _scoreController = new ScoreController();
            ServiceLocator.Current.Register(_scoreController);
            _disposables.Add(_scoreController);
        }

        
        // Для демонстрации подгрузки из JSON-ов
        private void InitJSONLoaders()
        {
            var shipLoader = new JsonShipLoader("ShipConfig.json");
            var levelLoader = new JsonLevelLoader("LevelConfig.json");

            ServiceLocator.Current.Register<ILevelLoader>(levelLoader);
            ServiceLocator.Current.Register<IShipDataLoader>(shipLoader);

            var loaders = new List<ILoader>();
            loaders.Add(levelLoader);
            loaders.Add(shipLoader);

            _configDataLoader.Init(loaders);
            ServiceLocator.Current.Register(_configDataLoader);
        }

        // Для демонстрации подгрузки из SO
        private void InitLocalLoaders()
        {
            ServiceLocator.Current.Register<IShipDataLoader>(_shipLoaderSO);
            ServiceLocator.Current.Register<ILevelLoader>(_levelLoaderSO);
            
            var loaders = new List<ILoader>();
            loaders.Add(_levelLoaderSO);
            loaders.Add(_shipLoaderSO);
            
            _configDataLoader.Init(loaders);
            ServiceLocator.Current.Register(_configDataLoader);
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}