namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал о том, что счёт изменился
    /// </summary>
    public class ScoreChangedSignal
    {
        public readonly int Score;

        public ScoreChangedSignal(int score)
        {
            Score = score;
        }
    }
}