using System.Collections.Generic;

/// <summary>
/// Загрузчик уровней из разных мест(ScriptableObject, JSON, Server)
/// </summary>
public interface ILevelLoader : IService, ILoader
{   
    public IEnumerable<LevelData> GetLevels();
}