using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentTypeRepository
    {
        #region Methods
        string GetStoreRepositoryClassName(Guid enchantmentDefinitionId);

        string GetDefinitionRepositoryClassName(Guid enchantmentDefinitionId);
        #endregion
    }
}
