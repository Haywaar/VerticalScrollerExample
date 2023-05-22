namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал "Выбери уровень level"
    /// </summary>
    public class SetLevelSignal
    {
        public readonly Level Level;

        public SetLevelSignal(Level level)
        {
            Level = level;
        }
    }
}