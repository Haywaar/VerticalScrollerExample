using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/LevelsConfig", order = 1)]
public class LevelsConfig : ScriptableObject
{
    [SerializeField] private List<LevelData> _levels;

    public List<LevelData> Levels => _levels;
}