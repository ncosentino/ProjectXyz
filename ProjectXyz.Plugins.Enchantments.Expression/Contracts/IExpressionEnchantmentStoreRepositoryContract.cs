using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentStoreRepository))]
    public abstract class IExpressionEnchantmentStoreRepositoryContract : IExpressionEnchantmentStoreRepository
    {
        #region Methods
        public IExpressionEnchantmentStore GetById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStore>() != null);

            return default(IExpressionEnchantmentStore);
        }

        public void Add(IExpressionEnchantmentStore expressionEnchantmentStore)
        {
            Contract.Requires<ArgumentNullException>(expressionEnchantmentStore != null);
        }

        public void RemoveById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
        }
        #endregion
    }
}
