namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал о том, что здоровье игрока изменилось
    /// </summary>
    public class HealthChangedSignal
    {
        public readonly int Health;
        public HealthChangedSignal(int health)
        {
            Health = health;
        }
    }
}