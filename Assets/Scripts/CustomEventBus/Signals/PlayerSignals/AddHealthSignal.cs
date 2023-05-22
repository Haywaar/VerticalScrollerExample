namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал "Прибавь здоровья игроку"
    /// </summary>
    public class AddHealthSignal
    {
        public readonly int Value;

        public AddHealthSignal(int value)
        {
            Value = value;
        }
    }
}