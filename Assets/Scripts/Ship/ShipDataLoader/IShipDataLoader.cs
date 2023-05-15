using System.Collections.Generic;

namespace Examples.VerticalScrollerExample.Scripts.Ship.ShipDataLoader
{
    public interface IShipDataLoader : IService
    {
        public IEnumerable<ShipData> GetShipsData();
        public ShipData GetCurrentShipData();
    }
}