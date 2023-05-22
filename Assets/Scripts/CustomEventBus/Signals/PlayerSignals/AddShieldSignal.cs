namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал "Добавь щит игроку на время ShieldDuration"
    /// </summary>
    public class AddShieldSignal
    {
        public readonly float ShieldDuration;

        public AddShieldSignal(float shieldDuration)
        {
            ShieldDuration = shieldDuration;
        }
    }
}