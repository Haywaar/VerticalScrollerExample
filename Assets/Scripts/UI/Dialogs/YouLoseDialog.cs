using Examples.VerticalScrollerExample;
using Examples.VerticalScrollerExample.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Окошко, отображающееся при проигрыше
/// </summary>
public class YouLoseDialog : Window
{
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _goToMenuButton;

    private void Awake()
    {
        _tryAgainButton.onClick.AddListener(TryAgain);
        _goToMenuButton.onClick.AddListener(GoToMenu);
    }

    private void TryAgain()
    {
        EventBus.Instance.StartLevel?.Invoke();
        Hide();
    }

    private void GoToMenu()
    {
        EventBus.Instance.GoToMenu?.Invoke();
    }
}
