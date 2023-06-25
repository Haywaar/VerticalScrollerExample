using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Отвечает за логику уровней:
/// Переключает текущий уровень на другой
/// Уведомляет остальные системы что уровень изменился
/// Уведомляет что уровень пройден
/// </summary>
public class LevelController : MonoBehaviour, IService
{
    private ILevelLoader _levelLoader;
    private int _currentLevelId;
    private Level _currentLevel;

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<LevelTimePassedSignal>(LevelPassed);
        _eventBus.Subscribe<NextLevelSignal>(NextLevel);

        _levelLoader = ServiceLocator.Current.Get<ILevelLoader>();
        _currentLevelId = PlayerPrefs.GetInt(StringConstants.CURRENT_LEVEL, 0);

        OnInit();
    }

    private async void OnInit()
    {
        await UniTask.WaitUntil(_levelLoader.IsLoaded);
        _currentLevel = _levelLoader.GetLevels().FirstOrDefault(x => x.ID == _currentLevelId);
        if (_currentLevel == null)
        {
            Debug.LogErrorFormat("Can't find level with id {0}", _currentLevelId);
            return;
        }
        _eventBus.Invoke(new SetLevelSignal(_currentLevel));
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
    }

    private void LevelPassed(LevelTimePassedSignal signal)
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
        _eventBus.Unsubscribe<NextLevelSignal>(NextLevel);
        _eventBus.Unsubscribe<LevelTimePassedSignal>(LevelPassed);
    }
}