using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentDefinitionRepositoryContract<>))]
    public interface IEnchantmentDefinitionRepository<TEnchantmentDefinition>
        where TEnchantmentDefinition : IEnchantmentDefinition
    {
        #region Methods
        void Add(TEnchantmentDefinition enchantmentDefinition);

        void RemoveById(Guid id);

        TEnchantmentDefinition GetById(Guid id);
        #endregion
    }
}
