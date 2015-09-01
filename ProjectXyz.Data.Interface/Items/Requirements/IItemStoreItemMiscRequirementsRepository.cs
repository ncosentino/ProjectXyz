using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemStoreItemMiscRequirementsRepository
    {
        #region Methods
        IItemStoreItemMiscRequirements Add(
            Guid id,
            Guid itemId,
            Guid itemMiscRequirementsId);

        void RemoveById(Guid id);

        IItemStoreItemMiscRequirements GetById(Guid id);

        IItemStoreItemMiscRequirements GetByItemId(Guid itemId);

        IEnumerable<IItemStoreItemMiscRequirements> GetAll();
        #endregion
    }
}
