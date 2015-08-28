using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemEnchantmentFactory
    {
        #region Methods
        IItemEnchantment Create(
            Guid id,
            Guid itemId,
            Guid enchantmentId);
        #endregion
    }
}
