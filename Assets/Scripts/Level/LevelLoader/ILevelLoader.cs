using System.Collections.Generic;

public interface ILevelLoader : IService
{
    public IEnumerable<Level> GetLevels();
}