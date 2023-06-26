using System.Collections.Generic;
using Interactables;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    /// <summary>
    /// Уникальный ID уровня
    /// </summary>
    [JsonProperty]
    [SerializeField] private int id;
    /// <summary>
    /// Длительность уровня в секундах
    /// </summary>
    [JsonProperty]
    [SerializeField] private float levelLength;
    /// <summary>
    /// Стартовая скорость падения препятствий и бонусов
    /// </summary>
    [JsonProperty]
    [SerializeField] private float startSpeed;
    /// <summary>
    /// Финальная скорость падения препятствий и бонусов
    /// </summary>
    [JsonProperty]
    [SerializeField] private float endSpeed;
    /// <summary>
    /// Количество голды за первое прохождение
    /// </summary>
    [JsonProperty]
    [SerializeField] private int goldForPass;
    /// <summary>
    /// Инфа о спавне препятствий и бонусов
    /// </summary>
    [JsonProperty]
    [SerializeField] private List<InteractableSpawnData> interactableSpawnData;
   
    public int ID => id;
    public float LevelLength => levelLength;
    public float StartSpeed => startSpeed;
    public float EndSpeed => endSpeed;
    public int GoldForPass => goldForPass;
    public List<InteractableSpawnData> InteractableData => interactableSpawnData;
}