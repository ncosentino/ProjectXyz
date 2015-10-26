using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemMetaDataFactory : IItemMetaDataFactory
    {
        #region Constructors
        private ItemMetaDataFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemMetaDataFactory Create()
        {
            var factory = new ItemMetaDataFactory();
            return factory;
        }

        public IItemMetaData Create(
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId)
        {
            var itemMetaData = ItemMetaData.Create(
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);
            return itemMetaData;
        }
        #endregion
    }
}
