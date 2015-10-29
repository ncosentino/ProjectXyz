using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentPluginFactory
    {
        #region Methods
        IEnchantmentPlugin Create(
            Guid id,
            Guid enchantmentTypeId,
            string storeRepositoryClassName,
            string definitionRepositoryClassName);
        #endregion
    }
}
