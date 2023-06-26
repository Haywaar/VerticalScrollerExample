namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал "Выбери уровень level"
    /// </summary>
    public class SetLevelSignal
    {
        public readonly LevelData LevelData;

        public SetLevelSignal(LevelData levelData)
        {
            LevelData = levelData;
        }
    }
}