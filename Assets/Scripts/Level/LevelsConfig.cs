using System.Collections.Generic;
using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/LevelsConfig", order = 1)]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<Level> _levels;

        public List<Level> Levels => _levels;
    }
}