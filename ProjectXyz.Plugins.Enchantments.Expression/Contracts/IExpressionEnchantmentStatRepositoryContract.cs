using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentStatRepository))]
    public abstract class IExpressionEnchantmentStatRepositoryContract : IExpressionEnchantmentStatRepository
    {
        #region Methods
        public IExpressionEnchantmentStat GetById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStat>() != null);

            return default(IExpressionEnchantmentStat);
        }

        public IEnumerable<IExpressionEnchantmentStat> GetByExpressionEnchantmentId(Guid expressionEnchantmentId)
        {
            Contract.Requires<ArgumentException>(expressionEnchantmentId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnumerable<IExpressionEnchantmentStat>>() != null);

            return default(IEnumerable<IExpressionEnchantmentStat>);
        }

        public void Add(IExpressionEnchantmentStat expressionEnchantmentStat)
        {
            Contract.Requires<ArgumentNullException>(expressionEnchantmentStat != null);
        }

        public void RemoveById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
        }

        public void RemoveByExpressionEnchantmentId(Guid expressionEnchantmentId)
        {
            Contract.Requires<ArgumentException>(expressionEnchantmentId != Guid.Empty);
        }
        #endregion
    }
}
