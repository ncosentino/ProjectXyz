using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixEnchantmentFactory
    {
        #region Methods
        IItemAffixEnchantment Create(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid enchantmentId);
        #endregion
    }
}
