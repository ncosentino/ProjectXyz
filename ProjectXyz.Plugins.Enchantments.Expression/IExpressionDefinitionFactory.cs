using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionDefinitionFactory
    {
        #region Methods
        IExpressionDefinition CreateExpressionDefinition(
            Guid id,
            string expression,
            int calculationPriority);
        #endregion
    }
}
