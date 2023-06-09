﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Dialogs
{
    /// <summary>
    /// Слот выбора уровня(используется в SelectlevelDialog)
    /// </summary>
    public class SelectLevelSlot : MonoBehaviour
    {
        [SerializeField] private Button _levelClickedButton;
        [SerializeField] private Text _maxScoreText;
        [SerializeField] private Text _levelText;

        public void Init(LevelData levelData)
        {
            _levelClickedButton.onClick.AddListener((() =>
            {
                PlayerPrefs.SetInt(StringConstants.CURRENT_LEVEL, (levelData.ID));
                SceneManager.LoadScene(StringConstants.MAIN_SCENE_NAME);
            }));

            _levelText.text = (levelData.ID + 1).ToString();

            var scoreController = ServiceLocator.Current.Get<ScoreController>();
            _maxScoreText.text = "Max score:" + scoreController.GetMaxScore(levelData.ID);
        }
    }
}