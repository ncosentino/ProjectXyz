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
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentDefinition>() != null);

            return default(IEnchantmentDefinition);
        }

        public IEnchantmentDefinition Add(
            Guid id,
            Guid triggerId,
            Guid statusTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentDefinition>() != null);
            
            return default(IEnchantmentDefinition);
        }

        public void RemoveById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
        }
        #endregion
    }
}
