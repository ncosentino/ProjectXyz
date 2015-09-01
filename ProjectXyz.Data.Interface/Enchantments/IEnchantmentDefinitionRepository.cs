using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentDefinitionRepositoryContract))]
    public interface IEnchantmentDefinitionRepository
    {
        #region Methods
        IEnchantmentDefinition Add(
            Guid id,
            Guid triggerId,
            Guid statusTypeId);

        void RemoveById(Guid id);

        IEnchantmentDefinition GetById(Guid id);
        #endregion
    }
}
