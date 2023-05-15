using System.Collections;
using System.Collections.Generic;

namespace Examples.VerticalScrollerExample
{
    public interface ILevelLoader : IService
    {
        public IEnumerable<Level> GetLevels();
    }
}