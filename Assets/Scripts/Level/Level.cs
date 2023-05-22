using System.Collections.Generic;
using Interactables;
using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField] private int _id;

    /// <summary>
    /// Длительность уровня в секундах
    /// </summary>
    [SerializeField] private float _levelLength;

    [SerializeField] private float _startSpeed;
    [SerializeField] private float _endSpeed;

    public float StartSpeed => _startSpeed;

    public float EndSpeed => _endSpeed;

    /// <summary>
    /// Количество голды за первое прохождение
    /// </summary>
    [SerializeField] private int _goldForPass;

    /// <summary>
    /// Инфа о спавне препятствий и бонусов
    /// </summary>
    [SerializeField] private List<InteractableData> _interactableData;

    public int ID => _id;
    public float LevelLength => _levelLength;
    public int GoldForPass => _goldForPass;
    public List<InteractableData> InteractableData => _interactableData;
}