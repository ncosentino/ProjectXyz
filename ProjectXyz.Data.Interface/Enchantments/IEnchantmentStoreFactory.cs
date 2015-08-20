using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentStoreFactory
    {
        #region Methods
        IEnchantmentStore Create(
            Guid id,
            Guid triggerId,
            Guid statusTypeId,
            Guid enchantmentTypeId,
            Guid enchantmentWeatherId);
        #endregion
    }
}
