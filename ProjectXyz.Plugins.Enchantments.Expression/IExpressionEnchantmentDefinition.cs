using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantmentDefinition
    {
        #region Properties
        Guid Id { get; }

        Guid EnchantmentDefinitionId { get; }
        
        Guid StatId { get; }

        Guid ExpressionId { get; }

        TimeSpan MinimumDuration { get; }

        TimeSpan MaximumDuration { get; }
        #endregion
    }
}