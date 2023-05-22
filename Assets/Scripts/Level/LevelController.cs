using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals;
using DefaultNamespace;
using UnityEngine;


/// <summary>
/// Отвечает за логику уровней
/// </summary>
public class LevelController : MonoBehaviour
{
    private ILevelLoader _levelLoader;
    private int _currentLevelId;
    private Level _currentLevel;

    private EventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<LevelTimePassedSignal>(LevelFinished);
        _eventBus.Subscribe<StartLevelSignal>(StartLevel);
        _eventBus.Subscribe<NextLevelSignal>(NextLevel);

        _levelLoader = ServiceLocator.Current.Get<ILevelLoader>();
        _currentLevelId = PlayerPrefs.GetInt(StringConstants.CURRENT_LEVEL, 0);

        _currentLevel = _levelLoader.GetLevels().FirstOrDefault(x => x.ID == _currentLevelId);
        _eventBus.Invoke(new SetLevelSignal(_currentLevel));
        StartLevel();
    }

    private void NextLevel(NextLevelSignal signal)
    {
        _currentLevelId++;
        SelectLevel(_currentLevelId);
    }

    private void SelectLevel(int level)
    {
        _currentLevelId = level;
        _currentLevel = _levelLoader.GetLevels().FirstOrDefault(x => x.ID == _currentLevelId);

        _eventBus.Invoke(new SetLevelSignal(_currentLevel));
        StartLevel();
    }


    private void StartLevel(StartLevelSignal signal)
    {
        StartLevel();
    }

    private void StartLevel()
    {
        ServiceLocator.Current.Get<GameController>().StartGame();
    }

    private void LevelFinished(LevelTimePassedSignal signal)
    {
        var player = ServiceLocator.Current.Get<Player>();
        if (player.Health > 0)
        {
            PlayerPrefs.SetInt(StringConstants.CURRENT_LEVEL, (_currentLevelId + 1));
            _eventBus.Invoke(new LevelFinishedSignal(_currentLevel));
        }
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<StartLevelSignal>(StartLevel);
        _eventBus.Unsubscribe<NextLevelSignal>(NextLevel);

        _eventBus.Unsubscribe<LevelTimePassedSignal>(LevelFinished);
    }
}