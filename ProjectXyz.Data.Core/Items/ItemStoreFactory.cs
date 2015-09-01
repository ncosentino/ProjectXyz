using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemStoreFactory : IItemStoreFactory
    {
        #region Constructors
        private ItemStoreFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemStoreFactory Create()
        {
            var factory = new ItemStoreFactory();
            return factory;
        }

        /// <inheritdoc />
        public IItemStore Create(
            Guid id,
            Guid itemDefinitionId,
            Guid nameStringResourceId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId)
        {
            var itemStore = ItemStore.Create(
                id,
                itemDefinitionId,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);
            return itemStore;
        }
        #endregion
    }
}
