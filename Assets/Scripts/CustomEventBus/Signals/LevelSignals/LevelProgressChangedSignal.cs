namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал о том что прогресс уровня изменился(в процентах)
    /// </summary>
    public class LevelProgressChangedSignal
    {
        public readonly float Progress;

        public LevelProgressChangedSignal(float progress)
        {
            Progress = progress;
        }
    }
}