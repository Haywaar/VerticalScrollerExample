using System.Collections;
using System.Collections.Generic;
using Examples.VerticalScrollerExample;
using Examples.VerticalScrollerExample.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Окошко при прохождении уровня игроком
/// </summary>
public class YouWinDialog : Window
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _goToMenuButton;
    [SerializeField] private Text _currentScoreText;
    [SerializeField] private Text _maxScoreText;
    [SerializeField] private Text _youObtainGoldText;
    
    void Awake()
    {
        _nextLevelButton.onClick.AddListener(NextLevel);
        _goToMenuButton.onClick.AddListener(GoToMenu);
    }

    public void Init(int currentScore, int maxScore, int addGoldValue)
    {
        _currentScoreText.text = "Your score: " + currentScore;
        _maxScoreText.text = "Max score: " + maxScore;
        _youObtainGoldText.text = "You received " + addGoldValue + " gold!";
    }

    private void NextLevel()
    {
        EventBus.Instance.NextLevel?.Invoke();
        Hide();
    }

    private void GoToMenu()
    {
        EventBus.Instance.GoToMenu?.Invoke(); 
        Hide();
    }
}
