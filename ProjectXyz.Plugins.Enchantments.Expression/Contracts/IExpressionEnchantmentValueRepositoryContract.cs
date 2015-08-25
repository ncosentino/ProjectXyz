using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentValueRepository))]
    public abstract class IExpressionEnchantmentValueRepositoryContract : IExpressionEnchantmentValueRepository
    {
        #region Methods
        public IExpressionEnchantmentValue GetById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValue>() != null);

            return default(IExpressionEnchantmentValue);
        }

        public IEnumerable<IExpressionEnchantmentValue> GetByExpressionEnchantmentId(Guid expressionEnchantmentId)
        {
            Contract.Requires<ArgumentException>(expressionEnchantmentId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnumerable<IExpressionEnchantmentValue>>() != null);

            return default(IEnumerable<IExpressionEnchantmentValue>);
        }

        public void Add(IExpressionEnchantmentValue expressionEnchantmentValue)
        {
            Contract.Requires<ArgumentNullException>(expressionEnchantmentValue != null);
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
