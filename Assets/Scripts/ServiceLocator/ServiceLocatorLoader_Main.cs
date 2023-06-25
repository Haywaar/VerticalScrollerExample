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
        [SerializeField] private InteractableMover _interactableMover;
        [SerializeField] private SignalSpawner _signalSpawner;
        [SerializeField] private InteractablesSpawner _interactablesSpawner;
        [SerializeField] private Player _player;
        [SerializeField] private GUIHolder _guiHolder;

        [SerializeField] private LevelController _levelController;
        [SerializeField] private TileMover _tileMover;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private ScriptableObjectLevelLoader _scriptableObjectLevelLoader;
        [SerializeField] private ScriptableObjectShipLoader _scriptableObjectShipLoader;
        private ConfigDataLoader _configDataLoader;

        [SerializeField] private bool _loadFromJSON;
        
        private EventBus _eventBus;
        private GameController _gameController;
        private GoldController _goldController;
        private ScoreController _scoreController;

        private IShipDataLoader _shipDataLoader;
        private ILevelLoader _levelLoader;

        private List<IDisposable> _disposables = new List<IDisposable>();

        private void Awake()
        {
            _eventBus = new EventBus();
            _gameController = new GameController();
            _goldController = new GoldController();
            _scoreController = new ScoreController();

            if (_loadFromJSON)
            {
                _levelLoader = new JsonLevelLoader("LevelConfig.json");
                _shipDataLoader = new JsonShipLoader("ShipConfig.json");
            }
            else
            {
                _levelLoader = _scriptableObjectLevelLoader;
                _shipDataLoader = _scriptableObjectShipLoader;
            }

            RegisterServices();
            Init();
            AddDisposables();
        }
        
        private void RegisterServices()
        {
            ServiceLocator.Initialize();

            ServiceLocator.Current.Register(_eventBus);
            ServiceLocator.Current.Register<InteractableMover>(_interactableMover);
            ServiceLocator.Current.Register<SignalSpawner>(_signalSpawner);
            ServiceLocator.Current.Register<InteractablesSpawner>(_interactablesSpawner);
            ServiceLocator.Current.Register<Player>(_player);
            ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
            ServiceLocator.Current.Register(_gameController);
            ServiceLocator.Current.Register(_goldController);
            ServiceLocator.Current.Register(_scoreController);
            ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);
            ServiceLocator.Current.Register<IShipDataLoader>(_shipDataLoader);
        }

        private void Init()
        {
            _interactableMover.Init();
            _signalSpawner.Init();
            _interactablesSpawner.Init();
            _player.Init();
            _gameController.Init();
            _goldController.Init();
            _scoreController.Init();
            _levelController.Init();
            _tileMover.Init();
            _healthBar.Init();
            
            var loaders = new List<ILoader>();
            loaders.Add(_levelLoader);
            loaders.Add(_shipDataLoader);
            _configDataLoader = new ConfigDataLoader();
            _configDataLoader.Init(loaders);
        }

        private void AddDisposables()
        {
            _disposables.Add(_gameController);
            _disposables.Add(_goldController);
            _disposables.Add(_scoreController);
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