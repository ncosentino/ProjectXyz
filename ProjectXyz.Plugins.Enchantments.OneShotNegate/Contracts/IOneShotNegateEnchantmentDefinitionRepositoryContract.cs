using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Contracts
{
    [ContractClassFor(typeof(IOneShotNegateEnchantmentDefinitionRepository))]
    public abstract class IOneShotNegateEnchantmentDefinitionRepositoryContract : IOneShotNegateEnchantmentDefinitionRepository
    {
        #region Methods
        public IOneShotNegateEnchantmentDefinition GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentDefinition>() != null);

            return default(IOneShotNegateEnchantmentDefinition);
        }

        public void Add(IOneShotNegateEnchantmentDefinition enchantmentDefinition)
        {
            Contract.Requires<ArgumentNullException>(enchantmentDefinition != null);
        }

        public abstract void RemoveById(Guid id);
        #endregion
    }
}
