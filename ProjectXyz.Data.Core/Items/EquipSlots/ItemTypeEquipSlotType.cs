using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.EquipSlots;

namespace ProjectXyz.Data.Core.Items.EquipSlots
{
    public sealed class ItemTypeEquipSlotType : IItemTypeEquipSlotType
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _itemTypeId;
        private readonly Guid _equipSlotTypeId;
        #endregion

        #region Constructors
        private ItemTypeEquipSlotType(
            Guid id,
            Guid itemTypeId,
            Guid equipSlotTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(equipSlotTypeId != Guid.Empty);
            
            _id = id;
            _itemTypeId = itemTypeId;
            _equipSlotTypeId = equipSlotTypeId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid ItemTypeId { get { return _itemTypeId; } }

        /// <inheritdoc />
        public Guid EquipSlotTypeId { get { return _equipSlotTypeId; } }
        #endregion

        #region Methods
        public static IItemTypeEquipSlotType Create(
            Guid id,
            Guid itemTypeId,
            Guid equipSlotTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(equipSlotTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemTypeEquipSlotType>() != null);

            var itemTypeEquipSlotType = new ItemTypeEquipSlotType(
                id,
                itemTypeId,
                equipSlotTypeId);
            return itemTypeEquipSlotType;
        }
        #endregion
    }
}
