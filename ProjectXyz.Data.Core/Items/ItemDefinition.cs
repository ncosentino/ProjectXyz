﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemDefinition : IItemDefinition
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _nameStringResourceId;
        private readonly Guid _inventoryGraphicResourceId;
        private readonly Guid _magicTypeId;
        private readonly Guid _itemTypeId;
        private readonly Guid _materialTypeId;
        private readonly Guid _socketTypeId;
        #endregion

        #region Constructors
        private ItemDefinition(
            Guid id,
            Guid nameStringResourceId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(inventoryGraphicResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(materialTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);

            _id = id;
            _nameStringResourceId = nameStringResourceId;
            _inventoryGraphicResourceId = inventoryGraphicResourceId;
            _magicTypeId = magicTypeId;
            _itemTypeId = itemTypeId;
            _materialTypeId = materialTypeId;
            _socketTypeId = socketTypeId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid NameStringResourceId { get { return _nameStringResourceId; } }

        /// <inheritdoc />
        public Guid InventoryGraphicResourceId { get { return _inventoryGraphicResourceId; } }

        /// <inheritdoc />
        public Guid MagicTypeId { get { return _magicTypeId; } }

        /// <inheritdoc />
        public Guid ItemTypeId { get { return _itemTypeId; } }

        /// <inheritdoc />
        public Guid MaterialTypeId { get { return _materialTypeId; } }

        /// <inheritdoc />
        public Guid SocketTypeId { get { return _socketTypeId; } }
        #endregion

        #region Methods
        public static IItemDefinition Create(
            Guid id,
            Guid nameStringResourceId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(inventoryGraphicResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(materialTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemDefinition>() != null);
            
            var itemDefinition = new ItemDefinition(
                id,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);
            return itemDefinition;
        }
        #endregion
    }
}