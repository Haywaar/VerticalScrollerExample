using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Dialogs
{
    /// <summary>
    /// Окно меню (используется на титульной сцене)
    /// </summary>
    public class MenuDialog : Window
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _selectLevelButton;
        [SerializeField] private Button _customizeShipButton;
        [SerializeField] private Button _highScoreButton;
        [SerializeField] private Button _settingsButton;

        protected void Awake()
        {
            base.Awake();
        
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _selectLevelButton.onClick.AddListener(OnSelectLevelButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
            _customizeShipButton.onClick.AddListener(OnCustomizeShipButtonClick);
            _highScoreButton.onClick.AddListener(OnHighscoreButtonClick);
        }

        private void OnPlayButtonClick()
        {
            SceneManager.LoadScene(StringConstants.MAIN_SCENE_NAME);
        }

        private void OnSelectLevelButtonClick()
        {
            WindowManager.ShowWindow<SelectLevelDialog>();
        }
    
        private void OnCustomizeShipButtonClick()
        {
            WindowManager.ShowWindow<CustomizeShipDialog>();
        }
    
        private void OnHighscoreButtonClick()
        {
            var levelLoader = ServiceLocator.Current.Get<ILevelLoader>();
            var levels = levelLoader.GetLevels().OrderBy(x => x.ID).ToList();
            var scoreTableDialog = WindowManager.GetWindow<ScoreTableDialog>();
            scoreTableDialog.Init(levels);
        }
    
        private void OnSettingsButtonClick()
        {
            WindowManager.ShowWindow<SettingsDialog>();
        }
    }
}
