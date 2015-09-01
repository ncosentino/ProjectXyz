using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemStoreFactory
    {
        #region Methods
        IItemStore Create(
            Guid id,
            Guid itemDefinitionId,
            Guid nameStringResourceId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId);
        #endregion
    }
}
