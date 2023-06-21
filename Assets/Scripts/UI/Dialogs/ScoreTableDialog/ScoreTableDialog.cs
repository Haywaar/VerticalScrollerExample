using System.Collections.Generic;
using UnityEngine;

namespace UI.Dialogs
{
    public class ScoreTableDialog : Window
    {
        [SerializeField] private ScoreTableSlot _slotPrefab;
        [SerializeField] private Transform _parent;

        public void Init(IEnumerable<Level> levels)
        {
            var scoreController = ServiceLocator.Current.Get<ScoreController>();
            
            foreach (var level in levels)
            {
                var slot = GameObject.Instantiate(_slotPrefab, _parent);
                var score = scoreController.GetMaxScore(level.ID);
                slot.Init((level.ID + 1), score);
            }
        }
    }
}