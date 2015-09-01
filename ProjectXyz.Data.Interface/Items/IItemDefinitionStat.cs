using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemDefinitionStat
    {
        #region Properties
        Guid Id { get; }

        Guid ItemDefinitionId { get; }

        Guid StatDefinitionId { get; }

        double MinimumValue { get; }

        double MaximumValue { get; }
        #endregion
    }
}
