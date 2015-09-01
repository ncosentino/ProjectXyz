using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemMetaData : IItemMetaData
    {
        #region Fields
        private readonly Guid _nameStringResourceId;
        private readonly Guid _inventoryGraphicResourceId;
        private readonly Guid _magicTypeId;
        private readonly Guid _itemTypeId;
        private readonly Guid _materialTypeId;
        private readonly Guid _socketTypeId;
        #endregion

        #region Constructors
        private ItemMetaData(
            Guid nameStringResourceId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId)
        {
            _nameStringResourceId = nameStringResourceId;
            _inventoryGraphicResourceId = inventoryGraphicResourceId;
            _magicTypeId = magicTypeId;
            _itemTypeId = itemTypeId;
            _materialTypeId = materialTypeId;
            _socketTypeId = socketTypeId;
        }
        #endregion

        #region Properties
        public Guid NameStringResourceId { get { return _nameStringResourceId;} }

        public Guid InventoryGraphicResourceId { get { return _inventoryGraphicResourceId; } }

        public Guid MagicTypeId { get { return _magicTypeId; } }

        public Guid ItemTypeId { get { return _itemTypeId; } }

        public Guid MaterialTypeId { get { return _materialTypeId; } }

        public Guid SocketTypeId { get { return _socketTypeId; } }
        #endregion

        #region Methods
        public static IItemMetaData Create(
            Guid nameStringResourceId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId)
        {
            var itemMetaData = new ItemMetaData(
                nameStringResourceId,
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
