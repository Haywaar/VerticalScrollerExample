using DefaultNamespace;
using Examples.VerticalScrollerExample;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Окно на игровой сцене где указывается номер уровня
/// время продержаться и счёт
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField] private Text _levelText;
    [SerializeField] private Image _levelProgressBar;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        EventBus.Instance.LevelSet += RedrawLevel;
        EventBus.Instance.ScoreChanged += RedrawScore;
        EventBus.Instance.LevelProgressChanged += RedrawLevelProgress;
        _exitButton.onClick.AddListener(GoToMenu);
    }

    private void RedrawLevel(Level level)
    {
        _levelText.text = "Level: " + (level.ID + 1).ToString();
    }

    private void RedrawScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    private void RedrawLevelProgress(float progressVal)
    {
        _levelProgressBar.fillAmount = progressVal;
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene(StringConstants.MENU_SCENE_NAME);
    }

    private void OnDestroy()
    {
        EventBus.Instance.LevelSet -= RedrawLevel;
        EventBus.Instance.ScoreChanged -= RedrawScore;
        EventBus.Instance.LevelProgressChanged -= RedrawLevelProgress;
        _exitButton.onClick.RemoveAllListeners();
    }
}
