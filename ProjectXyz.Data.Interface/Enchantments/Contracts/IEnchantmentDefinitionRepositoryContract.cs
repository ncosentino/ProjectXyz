using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentDefinitionRepository))]
    public abstract class IEnchantmentDefinitionRepositoryContract : IEnchantmentDefinitionRepository
    {
        #region Methods
        public IEnchantmentDefinition GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<IEnchantmentDefinition>() != null);

            return default(IEnchantmentDefinition);
        }

        public void Add(IEnchantmentDefinition enchantmentDefinition)
        {
            Contract.Requires<ArgumentNullException>(enchantmentDefinition != null);
        }

        public abstract void RemoveById(Guid id);
        #endregion
    }
}
