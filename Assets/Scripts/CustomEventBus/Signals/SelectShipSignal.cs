namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал - выбери корабль ShipID
    /// </summary>
    public class SelectShipSignal
    {
        public readonly int ShipId;

        public SelectShipSignal(int shipId)
        {
            ShipId = shipId;
        }
    }
}