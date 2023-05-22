using CustomEventBus;
using Examples.VerticalScrollerExample.Scripts.Ship.ShipDataLoader;
using UI;
using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    public class ServiceLocatorLoader_Menu : MonoBehaviour
    {
        [SerializeField] private ScriptableObjectLevelLoader _levelLoader;
        [SerializeField] private GUIHolder _guiHolder;
        [SerializeField] private ShipDataLoaderSO _shipDataLoader;
        
        private void Awake()
        {
            ServiceLocator.Initialize();
            
            var eventBus = new EventBus();
            ServiceLocator.Current.Register(eventBus);
            
            ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);
            ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
            ServiceLocator.Current.Register<IShipDataLoader>(_shipDataLoader);
            
            var goldController = new GoldController();
            ServiceLocator.Current.Register(goldController);
        }
    }
}