using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemEnchantment
    {
        #region Properties
        Guid Id { get; }

        Guid ItemId { get; }

        Guid EnchantmentId { get; }
        #endregion
    }
}
