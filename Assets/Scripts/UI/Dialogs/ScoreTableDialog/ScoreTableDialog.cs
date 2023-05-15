using System.Collections.Generic;
using DefaultNamespace;
using Examples.VerticalScrollerExample;
using Examples.VerticalScrollerExample.Scripts.UI;
using UnityEngine;

public class ScoreTableDialog : Window
{
    [SerializeField] private ScoreTableSlot _slotPrefab;
    [SerializeField] private Transform _parent;

    public void Init(IEnumerable<Level> levels)
    {
        foreach (var level in levels)
        {
            var slot = GameObject.Instantiate(_slotPrefab, _parent);
            var score = PlayerPrefs.GetInt(StringConstants.MAX_LEVEL_SCORE + level.ID, 0);
            slot.Init((level.ID + 1), score);
        }
    }
}