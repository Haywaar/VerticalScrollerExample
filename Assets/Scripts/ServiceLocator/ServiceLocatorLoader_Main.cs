using System.Collections.Generic;
using ConfigLoader.Ship;
using ConfigLoader.Ship.JSON;
using ConfigLoader.Ship.ScriptableObject;
using CustomEventBus;
using UI;
using UnityEngine;
using IDisposable = CustomEventBus.IDisposable;

namespace Examples.VerticalScrollerExample
{
    /// <summary>
    /// Загрузчик сервисов для сцены с игрой
    /// </summary>
    public class ServiceLocatorLoader_Main : MonoBehaviour
    {
        [SerializeField] private InteractableMover interactableMover;
        [SerializeField] private SignalSpawner _signalSpawner;
        [SerializeField] private InteractablesSpawner _interactablesSpawner;
        [SerializeField] private ScriptableObjectLevelLoader _levelLoader;
        [SerializeField] private Player _player;
        [SerializeField] private GUIHolder _guiHolder;
        [SerializeField] private ScriptableObjectShipLoader _scriptableObjectShipLoader;
        [SerializeField] private ConfigDataLoader _configDataLoader;


        private GameController _gameController;
        private GoldController _goldController;
        private ScoreController _scoreController;

        private List<IDisposable> _disposables = new List<IDisposable>();

        private void Awake()
        {
            ServiceLocator.Initialize();

            var eventBus = new EventBus();
            ServiceLocator.Current.Register(eventBus);

            ServiceLocator.Current.Register<InteractableMover>(interactableMover);
            ServiceLocator.Current.Register<SignalSpawner>(_signalSpawner);
            ServiceLocator.Current.Register<InteractablesSpawner>(_interactablesSpawner);
            ServiceLocator.Current.Register<Player>(_player);
            ServiceLocator.Current.Register<GUIHolder>(_guiHolder);

            InitJSONLoaders();
            //   InitLocalLoaders();

            _gameController = new GameController();
            ServiceLocator.Current.Register(_gameController);
            _disposables.Add(_gameController);

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
            var levelLoader = new JsonLevelLoader("LevelConfig.json");
            ServiceLocator.Current.Register<ILevelLoader>(levelLoader);

            var shipLoader = new JsonShipLoader("ShipConfig.json");
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
            ServiceLocator.Current.Register<IShipDataLoader>(_scriptableObjectShipLoader);
            ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);

            var loaders = new List<ILoader>();
            loaders.Add(_scriptableObjectShipLoader);
            loaders.Add(_levelLoader);

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