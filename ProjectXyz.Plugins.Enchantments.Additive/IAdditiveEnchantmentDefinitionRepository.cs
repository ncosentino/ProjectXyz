using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Additive;
using ProjectXyz.Plugins.Enchantments.Additive.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    [ContractClass(typeof(IAdditiveEnchantmentDefinitionRepositoryContract))]
    public interface IAdditiveEnchantmentDefinitionRepository
    {
        #region Methods
        void Add(IAdditiveEnchantmentDefinition enchantmentDefinition);

        void RemoveById(Guid id);

        IAdditiveEnchantmentDefinition GetById(Guid id);
        #endregion
    }
}
