using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentDefinitionRepository<>))]
    public abstract class IEnchantmentDefinitionRepositoryContract<TEnchantmentDefinition> : IEnchantmentDefinitionRepository<TEnchantmentDefinition>
        where TEnchantmentDefinition : IEnchantmentDefinition
    {
        #region Methods
        public TEnchantmentDefinition GetById(Guid id)
        {
            Contract.Ensures(Contract.Result<TEnchantmentDefinition>() != null);

            return default(TEnchantmentDefinition);
        }

        public void Add(TEnchantmentDefinition enchantmentDefinition)
        {
            Contract.Requires<ArgumentNullException>(enchantmentDefinition != null);
        }

        public abstract void RemoveById(Guid id);
        #endregion
    }
}
