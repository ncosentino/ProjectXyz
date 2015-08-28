using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixEnchantmentRepository
    {
        #region Methods
        IItemAffixEnchantment GetById(Guid id);

        IEnumerable<IItemAffixEnchantment> GetByItemAffixDefinitionId(Guid itemAffixDefinitionId);

        IEnumerable<IItemAffixEnchantment> GetAll();

        void Add(IItemAffixEnchantment itemAffixEnchantment);

        void RemoveById(Guid id);
        #endregion
    }
}
