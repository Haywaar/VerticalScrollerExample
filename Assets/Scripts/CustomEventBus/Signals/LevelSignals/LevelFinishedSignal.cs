namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал о том, что уровень завершён
    /// </summary>
    public class LevelFinishedSignal
    {
        public readonly LevelData LevelData;

        public LevelFinishedSignal(LevelData levelData)
        {
            LevelData = levelData;
        }
    }
}