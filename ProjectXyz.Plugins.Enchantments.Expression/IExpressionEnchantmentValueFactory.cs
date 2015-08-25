using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentValueFactoryContract))]
    public interface IExpressionEnchantmentValueFactory
    {
        #region Methods
        IExpressionEnchantmentValue CreateExpressionEnchantmentValue(
            Guid id,
            Guid expressionEnchantmentId,
            string idForExpression,
            double value);
        #endregion
    }
}
