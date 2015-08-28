using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixEnchantment
    {
        #region Properties
        Guid Id { get; }

        Guid ItemAffixDefinitionId { get; }

        Guid EnchantmentId { get; }
        #endregion
    }
}
