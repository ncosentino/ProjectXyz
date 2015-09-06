using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IStatsDataManager
    {
        #region Properties
        IStatDefinitionRepository StatDefinitions { get; }
        
        IStatRepository Stats { get; }

        IStatFactory StatFactory { get; }

        IStatCollectionFactory StatCollectionFactory { get; }
        #endregion
    }
}
