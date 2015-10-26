using System;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IItemMetaDataFactory
    {
        #region Methods
        IItemMetaData Create(
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId);
        #endregion
    }
}
