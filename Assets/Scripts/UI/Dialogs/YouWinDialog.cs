using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Dialogs
{
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
    
        private EventBus _eventBus;
    
        void Start()
        {
            _nextLevelButton.onClick.AddListener(NextLevel);
            _goToMenuButton.onClick.AddListener(GoToMenu);
        
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public void Init(int currentScore, int maxScore, int addGoldValue)
        {
            _currentScoreText.text = "Your score: " + currentScore;
            _maxScoreText.text = "Max score: " + maxScore;
            _youObtainGoldText.text = "You received " + addGoldValue + " gold!";
        }

        private void NextLevel()
        {
            _eventBus.Invoke(new NextLevelSignal());
            Hide();
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene(StringConstants.MENU_SCENE_NAME);
            Hide();
        }
    }
}
