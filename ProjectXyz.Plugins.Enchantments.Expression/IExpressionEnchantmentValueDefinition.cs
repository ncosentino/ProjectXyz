using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantmentValueDefinition
    {
        #region Properties
        Guid Id { get; set; }
        
        Guid EnchantmentDefinitionId { get; set; }
        
        string IdForExpression { get; set; }

        double MinimumValue { get; set; }

        double MaximumValue { get; set; }
        #endregion
    }
}