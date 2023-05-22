namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал о прибавлении счёта в игре
    /// </summary>
    public class AddScoreSignal
    {
        public readonly int Value;

        public AddScoreSignal(int value)
        {
            Value = value;
        }
    }
}