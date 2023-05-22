using CustomEventBus;
using CustomEventBus.Signals;
using DefaultNamespace;
using UI;
using UI.Dialogs;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Переключает состояние игры: игра/меню/окна и тд
/// </summary>
public class GameController : MonoBehaviour, IService
{
    private EventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<PlayerDeadSignal>(OnPlayerDead);
        _eventBus.Subscribe<LevelFinishedSignal>(LevelFinished);
        _eventBus.Subscribe<GoToMenuSignal>(GoToMenu);
    }

    private void OnPlayerDead(PlayerDeadSignal signal)
    {
        StopGame();
        WindowManager.ShowWindow<YouLoseDialog>();
    }

    public void StartGame()
    {
        _eventBus.Invoke(new GameStartedSignal());
    }

    public void StopGame()
    {
        _eventBus.Invoke(new GameStopSignal());
    }

    private void LevelFinished(LevelFinishedSignal signal)
    {
        var level = signal.Level;

        StopGame();
        _eventBus.Invoke(new AddGoldSignal(level.GoldForPass));

        var player = ServiceLocator.Current.Get<Player>();
        var score = player.Score;


        var maxScore = PlayerPrefs.GetInt(StringConstants.MAX_LEVEL_SCORE + level.ID, 0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt(StringConstants.MAX_LEVEL_SCORE + level.ID, score);
        }

        var youWinDialog = WindowManager.GetWindow<YouWinDialog>();
        youWinDialog.Init(score, maxScore, level.GoldForPass);
    }


    private void GoToMenu(GoToMenuSignal signal)
    {
        GoToMenu();
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene(StringConstants.MENU_SCENE_NAME);
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<PlayerDeadSignal>(OnPlayerDead);
        _eventBus.Unsubscribe<LevelFinishedSignal>(LevelFinished);
        _eventBus.Unsubscribe<GoToMenuSignal>(GoToMenu);
    }
}