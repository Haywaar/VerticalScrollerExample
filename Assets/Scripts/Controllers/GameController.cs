using CustomEventBus;
using CustomEventBus.Signals;
using UI;
using UI.Dialogs;

/// <summary>
/// Принимает решение о запуске и остановке игры
/// Уведомляет о старте/конце игры
/// </summary>
public class GameController : IService, IDisposable
{
    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PlayerDeadSignal>(OnPlayerDead);
        _eventBus.Subscribe<LevelFinishedSignal>(LevelFinished);
        _eventBus.Subscribe<SetLevelSignal>(StartGame, -1);
    }
    
    public void StartGame(SetLevelSignal signal)
    {
        _eventBus.Invoke(new GameStartedSignal());
    }

    public void StopGame()
    {
        _eventBus.Invoke(new GameStopSignal());
    }

    private void OnPlayerDead(PlayerDeadSignal signal)
    {
        StopGame();
        DialogManager.ShowDialog<YouLoseDialog>();
    }

    private void LevelFinished(LevelFinishedSignal signal)
    {
        var level = signal.Level;

        StopGame();

        // Показываем окошко о победе
        var scoreController = ServiceLocator.Current.Get<ScoreController>();
        YouWinDialog youWinDialog = DialogManager.GetDialog<YouWinDialog>();
        youWinDialog.Init(scoreController.Score, scoreController.GetMaxScore(level.ID), level.GoldForPass);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<PlayerDeadSignal>(OnPlayerDead);
        _eventBus.Unsubscribe<LevelFinishedSignal>(LevelFinished);
    }
}