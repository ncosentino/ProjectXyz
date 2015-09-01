using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IStatsDataStore
    {
        #region Properties
        IStatDefinitionRepository StatDefinitions { get; }
        
        IStatRepository Stats { get; }

        IStatFactory StatFactory { get; }
        #endregion
    }
}
