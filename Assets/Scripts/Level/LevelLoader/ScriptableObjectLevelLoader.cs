using System.Collections.Generic;
using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    public class ScriptableObjectLevelLoader : MonoBehaviour, ILevelLoader
    {
        [SerializeField] private LevelsConfig _levelsConfig;
        
        public IEnumerable<Level> GetLevels()
        {
            return _levelsConfig.Levels;
        }
    }
}