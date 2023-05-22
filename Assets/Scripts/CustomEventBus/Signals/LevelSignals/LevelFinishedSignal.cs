namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал о том, что уровень завершён
    /// </summary>
    public class LevelFinishedSignal
    {
        public readonly Level Level;

        public LevelFinishedSignal(Level level)
        {
            Level = level;
        }
    }
}