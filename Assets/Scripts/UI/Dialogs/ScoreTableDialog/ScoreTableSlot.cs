using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogs
{
    public class ScoreTableSlot: Window
    {
        [SerializeField] private Text _levelText;
        [SerializeField] private Text _levelScoreText;

        public void Init(int levelId, int levelScore)
        {
            _levelText.text = "Level " + levelId;
            _levelScoreText.text = levelScore.ToString();
        }

    }
}