using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IStatRange
    {
        #region Properties
        Guid StatDefinitionId { get; }

        double MinimumValue { get; }

        double MaximumValue { get; }
        #endregion
    }
}
