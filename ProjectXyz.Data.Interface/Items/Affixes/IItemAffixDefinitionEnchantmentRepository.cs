using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixDefinitionEnchantmentRepository
    {
        #region Methods
        IItemAffixDefinitionEnchantment GetById(Guid id);

        IEnumerable<IItemAffixDefinitionEnchantment> GetByItemAffixDefinitionId(Guid itemAffixDefinitionId);

        IEnumerable<IItemAffixDefinitionEnchantment> GetAll();

        IItemAffixDefinitionEnchantment Add(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid enchantmentDefinitionId);

        void RemoveById(Guid id);
        #endregion
    }
}
