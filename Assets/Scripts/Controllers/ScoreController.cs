using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

/// <summary>
/// Логика счёта в игре
/// Начисление счёта
/// Контроль за максимальным счётом на уровне
/// </summary>
public class ScoreController : IService, IDisposable
{
    private EventBus _eventBus;

    private int _score;
    public int Score => _score;

    public ScoreController()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<GameStartedSignal>(OnGameStarted);
        _eventBus.Subscribe<AddScoreSignal>(OnScoreAdded);
        _eventBus.Subscribe<LevelFinishedSignal>(OnLevelFinished);
    }

    private void OnGameStarted(GameStartedSignal signal)
    {
        _score = 0;
        _eventBus.Invoke(new ScoreChangedSignal(_score));
    }

    private void OnScoreAdded(AddScoreSignal signal)
    {
        _score += signal.Value;
        _eventBus.Invoke(new ScoreChangedSignal(_score));
    }

    // Сверяем текущий счёт уровня с максимальным, если что - обновляем рекорд
    private void OnLevelFinished(LevelFinishedSignal signal)
    {
        var level = signal.Level;
        var maxScore = GetMaxScore(level.ID);
        if (_score > maxScore)
        {
            PlayerPrefs.SetInt(StringConstants.MAX_LEVEL_SCORE + level.ID, _score);
        }
    }

    public int GetMaxScore(int levelID)
    {
        return PlayerPrefs.GetInt(StringConstants.MAX_LEVEL_SCORE + levelID, 0);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        _eventBus.Unsubscribe<AddScoreSignal>(OnScoreAdded);
        _eventBus.Unsubscribe<LevelFinishedSignal>(OnLevelFinished);
    }
}