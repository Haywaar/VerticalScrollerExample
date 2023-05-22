namespace CustomEventBus.Signals
{
    public class SpendGoldSignal
    {
        public readonly int Value;

        public SpendGoldSignal(int value)
        {
            Value = value;
        }
    }
}