using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IStatusNegation
    {
        #region Properties
        Guid StatId { get; }

        Guid EnchantmentStatusId { get; }
        #endregion
    }
}
