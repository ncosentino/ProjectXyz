using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemStoreRepository
    {
        #region Methods
        IItemStore Add(
            Guid id,
            Guid itemDefinitionId,
            Guid itemNamePartId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId);

        void RemoveById(Guid id);

        IItemStore GetById(Guid id);

        IEnumerable<IItemStore> GetAll();
        #endregion
    }
}
