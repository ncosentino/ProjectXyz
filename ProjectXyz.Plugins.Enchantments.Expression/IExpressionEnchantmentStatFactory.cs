using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentStatFactoryContract))]
    public interface IExpressionEnchantmentStatFactory
    {
        #region Methods
        IExpressionEnchantmentStat CreateExpressionEnchantmentStat(
            Guid id,
            Guid expressionEnchantmentId,
            string idForExpression,
            Guid statId);
        #endregion
    }
}
