using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixDefinitionEnchantmentFactory
    {
        #region Methods
        IItemAffixDefinitionEnchantment Create(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid enchantmentDefinitionId);
        #endregion
    }
}
