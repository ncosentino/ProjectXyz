using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantmentValueDefinition
    {
        #region Properties
        Guid Id { get; }
        
        Guid EnchantmentDefinitionId { get; }
        
        string IdForExpression { get; }

        double MinimumValue { get; }

        double MaximumValue { get; }
        #endregion
    }
}