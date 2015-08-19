using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Additive.Contracts
{
    [ContractClassFor(typeof(IAdditiveEnchantmentDefinitionRepository))]
    public abstract class IAdditiveEnchantmentDefinitionRepositoryContract : IAdditiveEnchantmentDefinitionRepository
    {
        #region Methods
        public IAdditiveEnchantmentDefinition GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentDefinition>() != null);

            return default(IAdditiveEnchantmentDefinition);
        }

        public void Add(IAdditiveEnchantmentDefinition enchantmentDefinition)
        {
            Contract.Requires<ArgumentNullException>(enchantmentDefinition != null);
        }

        public abstract void RemoveById(Guid id);
        #endregion
    }
}
