using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionDefinition
    {
        #region Properties
        Guid Id { get; set; }

        string Expression { get; set; }

        int CalculationPriority { get; set; }
        #endregion
    }
}