using System;
using DefaultNamespace;
using Examples.VerticalScrollerExample.Scripts.Player;
using Examples.VerticalScrollerExample.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Examples.VerticalScrollerExample
{
    /// <summary>
    /// Переключает состояние игры: игра/меню/окна и тд
    /// </summary>
    public class GameController : MonoBehaviour, IService
    {
        private void Awake()
        {
            EventBus.Instance.PlayerDead += OnPlayerDead;
            EventBus.Instance.LevelPassed += LevelFinished;
            EventBus.Instance.GoToMenu += GoToMenu;
        }
        
        private void OnPlayerDead()
        {
            StopGame();
            WindowManager.ShowWindow<YouLoseDialog>();
        }

        public void StartGame()
        {
            EventBus.Instance.GameStart?.Invoke();
        }

        public void StopGame()
        {
            EventBus.Instance.GameStop?.Invoke();
        }

        private void LevelFinished(Level level)
        {
            StopGame();
            EventBus.Instance.AddGold?.Invoke(level.GoldForPass);

            var player = ServiceLocator.Current.Get<Player>();
            var score = player.Score;
            
            
            var maxScore = PlayerPrefs.GetInt(StringConstants.MAX_LEVEL_SCORE + level.ID, 0);
            if (score > maxScore)
            {
                PlayerPrefs.SetInt(StringConstants.MAX_LEVEL_SCORE + level.ID, score);
            }
            
            var youWinDialog =  WindowManager.GetWindow<YouWinDialog>();
            youWinDialog.Init(score, maxScore, level.GoldForPass);
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene(StringConstants.MENU_SCENE_NAME);
        }

        private void OnDestroy()
        {
            EventBus.Instance.PlayerDead -= OnPlayerDead;
            EventBus.Instance.LevelPassed -= LevelFinished;
            EventBus.Instance.GoToMenu -= GoToMenu;
        }
    }
}