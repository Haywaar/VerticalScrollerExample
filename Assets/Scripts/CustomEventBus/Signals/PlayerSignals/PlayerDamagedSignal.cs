namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал о том что игрок получил урон на Health
    /// </summary>
    public class PlayerDamagedSignal
    {
        public readonly int Health;

        public PlayerDamagedSignal(int health)
        {
            Health = health;
        }
    }
}