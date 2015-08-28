using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemEnchantmentRepository
    {
        #region Methods
        IItemEnchantment Add(
            Guid id,
            Guid itemId,
            Guid enchantmentId);

        void RemoveById(Guid id);

        IItemEnchantment GetById(Guid id);

        IEnumerable<IItemEnchantment> GetByItemId(Guid id);

        IEnumerable<IItemEnchantment> GetAll();
        #endregion
    }
}
