using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Percentage;
using ProjectXyz.Plugins.Enchantments.Percentage.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    [ContractClass(typeof(IPercentageEnchantmentDefinitionRepositoryContract))]
    public interface IPercentageEnchantmentDefinitionRepository
    {
        #region Methods
        void Add(IPercentageEnchantmentDefinition enchantmentDefinition);

        void RemoveById(Guid id);

        IPercentageEnchantmentDefinition GetById(Guid id);
        #endregion
    }
}
