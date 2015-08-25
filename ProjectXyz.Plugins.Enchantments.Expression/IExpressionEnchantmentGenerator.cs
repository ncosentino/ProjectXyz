using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantmentGenerator
    {
        #region Methods
        IExpressionEnchantment Generate(
            IRandom randomizer,
            IEnchantmentDefinition enchantmentDefinition);
        #endregion
    }
}
