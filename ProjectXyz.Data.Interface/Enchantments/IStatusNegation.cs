using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IStatusNegation
    {
        #region Properties
        Guid Id { get; }

        Guid StatId { get; }

        Guid EnchantmentStatusId { get; }
        #endregion
    }
}
