using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantmentStatDefinition
    {
        #region Properties
        Guid Id { get; }
        
        Guid EnchantmentDefinitionId { get; }
        
        string IdForExpression { get; }

        Guid StatId { get; }
        #endregion
    }
}