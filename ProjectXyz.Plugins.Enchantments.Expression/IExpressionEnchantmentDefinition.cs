using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantmentDefinition
    {
        #region Properties
        Guid Id { get; set; }

        Guid EnchantmentDefinitionId { get; set; }
        
        Guid StatId { get; set; }

        string Expression { get; set; }

        TimeSpan MinimumDuration { get; set; }

        TimeSpan MaximumDuration { get; set; }
        #endregion
    }
}