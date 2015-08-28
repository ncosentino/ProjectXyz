using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IStat
    {
        #region Properties
        Guid Id { get; }

        Guid StatDefinitionId { get; }

        double Value { get; }
        #endregion
    }
}
