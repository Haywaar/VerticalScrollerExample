using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Examples.VerticalScrollerExample
{
    /// <summary>
    /// Слот выбора уровня(используется в SelectlevelDialog)
    /// </summary>
    public class SelectLevelSlot : MonoBehaviour
    {
        [SerializeField] private Button _levelClickedButton;
        [SerializeField] private Text _maxScoreText;
        [SerializeField] private Text _levelText;

        public void Init(Level level)
        {
            _levelClickedButton.onClick.AddListener((() =>
            {
                PlayerPrefs.SetInt(StringConstants.CURRENT_LEVEL, (level.ID));
                SceneManager.LoadScene(StringConstants.MAIN_SCENE_NAME);
            }));

            _levelText.text = (level.ID + 1).ToString();

            _maxScoreText.text = "Max score:" + PlayerPrefs.GetInt(StringConstants.MAX_LEVEL_SCORE + level.ID, 0);
        }
    }
}