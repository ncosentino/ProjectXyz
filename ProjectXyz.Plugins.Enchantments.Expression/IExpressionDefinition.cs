using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionDefinition
    {
        #region Properties
        Guid Id { get; }

        string Expression { get; }

        int CalculationPriority { get; }
        #endregion
    }
}