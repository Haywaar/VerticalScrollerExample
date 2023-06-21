using System.Collections.Generic;
using Examples.VerticalScrollerExample.Scripts.Ship;

namespace ConfigLoader.Ship
{
    public interface IShipDataLoader : IService, ILoader
    {
        public IEnumerable<ShipData> GetShipsData();
        public ShipData GetCurrentShipData();
    }
}