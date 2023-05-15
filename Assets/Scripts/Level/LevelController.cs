using System.Linq;
using DefaultNamespace;
using Examples.VerticalScrollerExample.Scripts.Player;
using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    public class LevelController : MonoBehaviour
    {
        private ILevelLoader _levelLoader;
        private int _currentLevelId;
        private Level _currentLevel;

        private void Awake()
        {
            EventBus.Instance.LevelRestart += RestartLevel;
            EventBus.Instance.LevelTimePassed += LevelFinished;
            EventBus.Instance.NextLevel += NextLevel;
            EventBus.Instance.SelectShip += SelectLevel;
        }

        private void NextLevel()
        {
            _currentLevelId++;
            SelectLevel(_currentLevelId);
        }

        private void SelectLevel(int level)
        {
            _currentLevelId = level;
            _currentLevel = _levelLoader.GetLevels().FirstOrDefault(x => x.ID == _currentLevelId);

            EventBus.Instance.LevelSet?.Invoke(_currentLevel);
            ServiceLocator.Current.Get<GameController>().StartGame();
        }

        private void Start()
        {
            _levelLoader = ServiceLocator.Current.Get<ILevelLoader>();
            _currentLevelId = PlayerPrefs.GetInt(StringConstants.CURRENT_LEVEL, 0);

            _currentLevel = _levelLoader.GetLevels().FirstOrDefault(x => x.ID == _currentLevelId);
            EventBus.Instance.LevelSet?.Invoke(_currentLevel);
            ServiceLocator.Current.Get<GameController>().StartGame();
        }
        
        private void RestartLevel()
        {
            ServiceLocator.Current.Get<GameController>().StartGame();
        }
        
        private void LevelFinished()
        {
            var player = ServiceLocator.Current.Get<Player>();
            if (player.Health > 0)
            {
                PlayerPrefs.SetInt(StringConstants.CURRENT_LEVEL, (_currentLevelId + 1));
                EventBus.Instance.LevelPassed?.Invoke(_currentLevel);
            }
        }

        private void OnDestroy()
        {
            EventBus.Instance.LevelRestart -= RestartLevel;
            EventBus.Instance.LevelTimePassed -= LevelFinished;
            EventBus.Instance.NextLevel -= NextLevel;
            EventBus.Instance.SelectShip -= SelectLevel;
        }
    }
}