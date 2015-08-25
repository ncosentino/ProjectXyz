using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentStatFactory))]
    public abstract class IExpressionEnchantmentStatFactoryContract : IExpressionEnchantmentStatFactory
    {
        #region Methods
        public IExpressionEnchantmentStat CreateExpressionEnchantmentStat(
            Guid id,
            Guid expressionEnchantmentId,
            string idForExpression,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionEnchantmentId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(idForExpression));
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStat>() != null);

            return default(IExpressionEnchantmentStat);
        }
        #endregion
    }
}
