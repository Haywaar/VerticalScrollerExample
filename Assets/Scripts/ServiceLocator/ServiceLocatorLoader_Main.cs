using Examples.VerticalScrollerExample.Scripts.Player;
using Examples.VerticalScrollerExample.Scripts.Ship.ShipDataLoader;
using Examples.VerticalScrollerExample.Scripts.UI;
using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    /// <summary>
    /// Загрузчик сервисов для сцены с игрой
    /// </summary>
    public class ServiceLocatorLoader_Main : MonoBehaviour
    {
        [SerializeField] private MovementController _movementController;
        [SerializeField] private SignalSpawner _signalSpawner;
        [SerializeField] private InteractablesSpawner _interactablesSpawner;
        [SerializeField] private ScriptableObjectLevelLoader _levelLoader;
        [SerializeField] private Player _player;
        [SerializeField] private GUIHolder _guiHolder;
        [SerializeField] private ShipDataLoaderSO _shipDataLoader;
        [SerializeField] private GameController _gameController;
        private void Awake()
        {
            ServiceLocator.Initialize();
            
            ServiceLocator.Current.Register<MovementController>(_movementController);
            ServiceLocator.Current.Register<SignalSpawner>(_signalSpawner);
            ServiceLocator.Current.Register<InteractablesSpawner>(_interactablesSpawner);
            ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);
            ServiceLocator.Current.Register<Player>(_player);
            ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
            ServiceLocator.Current.Register<IShipDataLoader>(_shipDataLoader);
            ServiceLocator.Current.Register<GameController>(_gameController);

            var goldController = new GoldController();
            ServiceLocator.Current.Register(goldController);
        }
    }
}