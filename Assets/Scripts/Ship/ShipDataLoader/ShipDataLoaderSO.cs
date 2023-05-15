using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Examples.VerticalScrollerExample.Scripts.Ship.ShipDataLoader
{
    /// <summary>
    /// Загружает данные о кораблях из монобеха
    /// </summary>
    public class ShipDataLoaderSO : MonoBehaviour, IShipDataLoader
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
    }
}