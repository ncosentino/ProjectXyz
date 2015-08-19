using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Percentage.Contracts
{
    [ContractClassFor(typeof(IPercentageEnchantmentDefinitionRepository))]
    public abstract class IPercentageEnchantmentDefinitionRepositoryContract : IPercentageEnchantmentDefinitionRepository
    {
        #region Methods
        public IPercentageEnchantmentDefinition GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<IPercentageEnchantmentDefinition>() != null);

            return default(IPercentageEnchantmentDefinition);
        }

        public void Add(IPercentageEnchantmentDefinition enchantmentDefinition)
        {
            Contract.Requires<ArgumentNullException>(enchantmentDefinition != null);
        }

        public abstract void RemoveById(Guid id);
        #endregion
    }
}
