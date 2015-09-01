using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixDefinitionEnchantment
    {
        #region Properties
        Guid Id { get; }

        Guid ItemAffixDefinitionId { get; }

        Guid EnchantmentDefinitionId { get; }
        #endregion
    }
}
