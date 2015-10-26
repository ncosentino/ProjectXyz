using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items.Affixes
{
    public interface IItemAffixFactory
    {
        #region Methods
        IItemAffix Create(
            Guid itemAffixDefinitionId,
            Guid nameStringResourceId,
            bool prefix,
            IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
