using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentValueFactory))]
    public abstract class IExpressionEnchantmentValueFactoryContract : IExpressionEnchantmentValueFactory
    {
        #region Methods
        public IExpressionEnchantmentValue CreateExpressionEnchantmentValue(
            Guid id,
            Guid expressionEnchantmentId,
            string idForExpression,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionEnchantmentId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(idForExpression));
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValue>() != null);

            return default(IExpressionEnchantmentValue);
        }
        #endregion
    }
}
