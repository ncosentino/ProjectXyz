using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.OneShotNegate;
using ProjectXyz.Plugins.Enchantments.OneShotNegate.Contracts;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    [ContractClass(typeof(IOneShotNegateEnchantmentDefinitionRepositoryContract))]
    public interface IOneShotNegateEnchantmentDefinitionRepository
    {
        #region Methods
        void Add(IOneShotNegateEnchantmentDefinition enchantmentDefinition);

        void RemoveById(Guid id);

        IOneShotNegateEnchantmentDefinition GetById(Guid id);
        #endregion
    }
}
