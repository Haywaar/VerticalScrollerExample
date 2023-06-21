using System.Collections.Generic;
using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals;
using Examples.VerticalScrollerExample.Scripts.Ship;
using UnityEngine;

namespace ConfigLoader.Ship.ScriptableObject
{
    /// <summary>
    /// Загружает данные о кораблях из монобеха
    /// </summary>
    public class ScriptableObjectShipLoader : MonoBehaviour, IShipDataLoader
    {
        [SerializeField] private ShipDataConfig _config;
        
        public IEnumerable<ShipData> GetShipsData()
        {
            return _config.ShipsData;
        }

        public ShipData GetCurrentShipData()
        {
            var id = PlayerPrefs.GetInt(StringConstants.SELECTED_SHIP, 0);
            return _config.ShipsData.FirstOrDefault(x => x.ID == id);
        }

        public bool IsLoaded()
        {
            return true;
        }

        public void Load()
        {
            var eventBus = ServiceLocator.Current.Get<EventBus>();
            eventBus.Invoke(new DataLoadedSignal(this));
        }

        public bool IsLoadingInstant()
        {
            return true;
        }
    }
}